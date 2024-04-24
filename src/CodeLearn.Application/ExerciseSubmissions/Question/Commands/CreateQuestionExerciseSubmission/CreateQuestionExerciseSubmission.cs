using CodeLearn.Domain.Exercises.ValueObjects;
using CodeLearn.Domain.ExerciseSubmissions;
using CodeLearn.Domain.TestingSessions.ValueObjects;

namespace CodeLearn.Application.ExerciseSubmissions.Question.Commands.CreateQuestionExerciseSubmission;

public record CreateQuestionExerciseSubmissionCommand(int TestingSessionId, int ExerciseId, int[] SelectedAnswers) 
    : IRequest<OneOf<long, ValidationFailed, NotFound>>;

public class CreateQuestionExerciseSubmissionCommandHandler(
    IApplicationDbContext _context,
    IValidator<CreateQuestionExerciseSubmissionCommand> _validator)
    : IRequestHandler<CreateQuestionExerciseSubmissionCommand, OneOf<long, ValidationFailed, NotFound>>
{
    public async Task<OneOf<long, ValidationFailed, NotFound>> Handle(CreateQuestionExerciseSubmissionCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return new ValidationFailed(validationResult.Errors);
        }

        var testingSessionExists = await _context.TestingSessions
            .AnyAsync(x => x.Id == TestingSessionId.Create(request.TestingSessionId), cancellationToken);

        if (!testingSessionExists)
        {
            return new NotFound();
        }

        var exerciseExists = await _context.QuestionExercises
            .AnyAsync(x => x.Id == ExerciseId.Create(request.ExerciseId), cancellationToken);

        if (!exerciseExists)
        {
            return new NotFound();
        }

        // TODO: check for conflict if already exists

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