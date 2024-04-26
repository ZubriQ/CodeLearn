using CodeLearn.Domain.Exercises.ValueObjects;
using CodeLearn.Domain.ExerciseSubmissions;
using CodeLearn.Domain.ExerciseSubmissions.Enums;
using CodeLearn.Domain.TestingSessions.ValueObjects;

namespace CodeLearn.Application.ExerciseSubmissions.MethodCoding.Commands.CreateMethodCodingExerciseSubmission;

public record CreateMethodCodingExerciseSubmissionCommand(
    int TestingSessionId,
    int ExerciseId,
    string MethodSolutionCode)
    : IRequest<OneOf<string, ValidationFailed, NotFound>>;

public class CreateMethodCodingExerciseSubmissionCommandHandler(
    IApplicationDbContext _context,
    IValidator<CreateMethodCodingExerciseSubmissionCommand> _validator,
    ICodeTesterService _codeTesterService)
    : IRequestHandler<CreateMethodCodingExerciseSubmissionCommand, OneOf<string, ValidationFailed, NotFound>>
{
    public async Task<OneOf<string, ValidationFailed, NotFound>> Handle(CreateMethodCodingExerciseSubmissionCommand request, CancellationToken cancellationToken)
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
        var testingTask = Task.Run(async () =>
        {
            try
            {
                var testingResult = await _codeTesterService.TestMethodAsync(exercise, request.MethodSolutionCode);
                return testingResult;
            }
            catch (Exception)
            {
                return false;
            }
        }, cancellationToken);

        // Wait for the testing task to complete, with a timeout to prevent indefinite waiting
        if (await Task.WhenAny(testingTask, Task.Delay(TimeSpan.FromSeconds(2), cancellationToken)) == testingTask)
        {
            var testingResult = await testingTask;
            if (testingResult)
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
            submission.SetStatus(SubmissionTestStatus.Timeout);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return submission.Status.ToString();
    }
}