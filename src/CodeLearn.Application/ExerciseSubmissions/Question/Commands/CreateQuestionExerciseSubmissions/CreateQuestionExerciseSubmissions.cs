using CodeLearn.Domain.Exercises.ValueObjects;
using CodeLearn.Domain.ExerciseSubmissions;
using CodeLearn.Domain.ExerciseSubmissions.JunctionTables;
using CodeLearn.Domain.QuestionChoices;
using CodeLearn.Domain.TestingSessions.ValueObjects;

namespace CodeLearn.Application.ExerciseSubmissions.Question.Commands.CreateQuestionExerciseSubmissions;

public record CreateQuestionExerciseSubmissionsCommand(int TestingSessionId, Dictionary<int, List<int>> SelectedChoices)
    : IRequest<OneOf<Success, ValidationFailed, NotFound>>;

public class CreateQuestionExerciseSubmissionsCommandHandler(
    IApplicationDbContext _context,
    IValidator<CreateQuestionExerciseSubmissionsCommand> _validator)
    : IRequestHandler<CreateQuestionExerciseSubmissionsCommand, OneOf<Success, ValidationFailed, NotFound>>
{
    public async Task<OneOf<Success, ValidationFailed, NotFound>> Handle(CreateQuestionExerciseSubmissionsCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return new ValidationFailed(validationResult.Errors);
        }

        foreach (var (exerciseId, selectedChoicesList) in request.SelectedChoices)
        {
            var exerciseSubmission = ChoiceExerciseSubmission.Create(
                ExerciseId.Create(exerciseId), TestingSessionId.Create(request.TestingSessionId), DateTimeOffset.UtcNow);

            _context.ExerciseSubmissions.Add(exerciseSubmission);

            foreach (var choiceId in selectedChoicesList)
            {
                if (await _context.QuestionChoices.FindAsync([choiceId], cancellationToken) is not QuestionChoice choice)
                {
                    return new NotFound();
                }

                var selectedChoice = new SelectedChoice
                {
                    ExerciseSubmissionId = exerciseSubmission.Id,
                    QuestionChoiceId = choice.Id
                };

                _context.SelectedChoices.Add(selectedChoice);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        return new Success();
    }
}