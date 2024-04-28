using CodeLearn.Domain.Common.Result;
using CodeLearn.Domain.Exercises.ValueObjects;
using CodeLearn.Domain.ExerciseSubmissions;
using CodeLearn.Domain.ExerciseSubmissions.Enums;
using CodeLearn.Domain.TestingSessions.Enums;
using CodeLearn.Domain.TestingSessions.ValueObjects;

namespace CodeLearn.Application.ExerciseSubmissions.MethodCoding.Commands.CreateMethodCodingExerciseSubmission;

public record CreateMethodCodingExerciseSubmissionCommand(
    int TestingSessionId,
    int ExerciseId,
    string MethodSolutionCode)
    : IRequest<OneOf<Result, ValidationFailed, NotFound>>;

public class CreateMethodCodingExerciseSubmissionCommandHandler(
    IApplicationDbContext _context,
    IValidator<CreateMethodCodingExerciseSubmissionCommand> _validator,
    ICodeTesterService _codeTesterService)
    : IRequestHandler<CreateMethodCodingExerciseSubmissionCommand, OneOf<Result, ValidationFailed, NotFound>>
{
    public async Task<OneOf<Result, ValidationFailed, NotFound>> Handle(CreateMethodCodingExerciseSubmissionCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return new ValidationFailed(validationResult.Errors);
        }

        var testingSession = await _context.TestingSessions
            .FirstOrDefaultAsync(x => x.Id == TestingSessionId.Create(request.TestingSessionId), cancellationToken);

        if (testingSession is null)
        {
            return new NotFound();
        }

        if (testingSession.Status is TestingSessionStatus.Finished)
        {
            validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(
                "Finished", "Testing sessions is finished."));
            return new ValidationFailed(validationResult.Errors);
        }

        if (testingSession.FinishDateTime.ToUniversalTime() < DateTimeOffset.UtcNow)
        {
            testingSession.Finish();

            validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(
                "Finished", "Testing sessions is finished."));
            return new ValidationFailed(validationResult.Errors);
        }

        var exercise = await _context.MethodCodingExercises
            .Include(x => x.MethodReturnDataType)
            .Include(x => x.MethodParameters)
            .ThenInclude(y => y.DataType)
            .FirstOrDefaultAsync(x => x.Id == ExerciseId.Create(request.ExerciseId), cancellationToken);

        if (exercise is null)
        {
            return new NotFound();
        }

        var submission = CodeExerciseSubmission.Create(
            ExerciseId.Create(request.ExerciseId),
            TestingSessionId.Create(request.TestingSessionId),
            DateTimeOffset.UtcNow,
            request.MethodSolutionCode);

        await _context.CodeExerciseSubmissions.AddAsync(submission, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        // Test method coding exercise submission
        Result testingResult = null!;

        var testingTask = Task.Run(async () =>
        {
            try
            {
                var result = await _codeTesterService.TestMethodAsync(exercise, request.MethodSolutionCode);
                testingResult = result.IsSuccess ? Result.Success() : Result.Failure(result.Error);
            }
            catch (Exception)
            {
                testingResult = Result.Failure(ApplicationErrors.ExerciseSubmissions.UnexpectedCodeTesterServiceException);
            }
        }, cancellationToken);

        // Wait for the testing task to complete, with a timeout to prevent indefinite waiting
        var completedTask = await Task.WhenAny(testingTask, Task.Delay(TimeSpan.FromSeconds(2), cancellationToken));
        if (completedTask == testingTask)
        {
            if (testingResult.IsSuccess)
            {
                submission.SetStatus(SubmissionTestStatus.Solved);
            }
            else
            {
                submission.SetStatus(SubmissionTestStatus.Unsolved);
            }
        }
        else
        {
            testingResult = Result.Failure(ApplicationErrors.ExerciseSubmissions.TestingTimeout);
            submission.SetStatus(SubmissionTestStatus.Timeout);
        }

        submission.SetTestingInformation(testingResult);

        await _context.SaveChangesAsync(cancellationToken);

        return testingResult;
    }
}