using CodeLearn.Domain.Exercises.ValueObjects;
using CodeLearn.Domain.ExerciseSubmissions;
using CodeLearn.Domain.TestingSessions.Enums;
using CodeLearn.Domain.TestingSessions.ValueObjects;

namespace CodeLearn.Application.ExerciseSubmissions.Question.Commands.CreateQuestionExerciseSubmission;

public record CreateQuestionExerciseSubmissionCommand(int TestingSessionId, int ExerciseId, int[] SelectedAnswers)
    : IRequest<OneOf<long, ValidationFailed, NotFound, Conflict>>;

public class CreateQuestionExerciseSubmissionCommandHandler(
    IApplicationDbContext _context,
    IValidator<CreateQuestionExerciseSubmissionCommand> _validator)
    : IRequestHandler<CreateQuestionExerciseSubmissionCommand, OneOf<long, ValidationFailed, NotFound, Conflict>>
{
    public async Task<OneOf<long, ValidationFailed, NotFound, Conflict>> Handle(CreateQuestionExerciseSubmissionCommand request, CancellationToken cancellationToken)
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

            await _context.SaveChangesAsync(cancellationToken);

            validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(
                "Finished", "Testing sessions is finished."));
            return new ValidationFailed(validationResult.Errors);
        }

        var exerciseExists = await _context.QuestionExercises
            .AnyAsync(x => x.Id == ExerciseId.Create(request.ExerciseId), cancellationToken);

        if (!exerciseExists)
        {
            return new NotFound();
        }

        var submissionAlreadyExists = await _context.ChoiceExerciseSubmissions
            .AnyAsync(x => x.ExerciseId == ExerciseId.Create(request.ExerciseId)
                           && x.TestingSessionId == TestingSessionId.Create(request.TestingSessionId), cancellationToken);

        if (submissionAlreadyExists)
        {
            return new Conflict();
        }

        var exerciseSubmission = ChoiceExerciseSubmission.Create(
                ExerciseId.Create(request.ExerciseId),
                TestingSessionId.Create(request.TestingSessionId),
                DateTimeOffset.UtcNow);

        var questionChoices = await _context.QuestionChoices
            .Where(x => x.ExerciseId == ExerciseId.Create(request.ExerciseId))
            .ToListAsync(cancellationToken);

        foreach (var choice in questionChoices)
        {
            exerciseSubmission.AddChoice(choice);
        }

        await _context.ChoiceExerciseSubmissions.AddAsync(exerciseSubmission, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return exerciseSubmission.Id.Value;
    }
}