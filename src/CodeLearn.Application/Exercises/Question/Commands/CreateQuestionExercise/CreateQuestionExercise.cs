using CodeLearn.Domain.Exercises;
using CodeLearn.Domain.Exercises.Enums;
using CodeLearn.Domain.ExerciseTopics.ValueObjects;
using CodeLearn.Domain.QuestionChoices;
using CodeLearn.Domain.Tests.ValueObjects;
using FluentValidation.Results;

namespace CodeLearn.Application.Exercises.QuestionExercises.Commands.CreateQuestionExercise;

public record CreateQuestionExerciseCommand(
    int TestId,
    string Title,
    string Description,
    string Difficulty,
    int[] ExerciseTopics,
    List<AnswerDto> Answers)
    : IRequest<OneOf<int, ValidationFailed, NotFound>>;

public class CreateQuestionExerciseCommandHandler(
    IApplicationDbContext _context,
    IValidator<CreateQuestionExerciseCommand> _validator)
    : IRequestHandler<CreateQuestionExerciseCommand, OneOf<int, ValidationFailed, NotFound>>
{
    public async Task<OneOf<int, ValidationFailed, NotFound>> Handle(CreateQuestionExerciseCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return new ValidationFailed(validationResult.Errors);
        }

        if (!Enum.TryParse<ExerciseDifficulty>(request.Difficulty, true, out var difficultyEnum))
        {
            List<ValidationFailure> validationFailure = ValidateEnum(request);

            return new ValidationFailed(validationFailure);
        }

        var questionExercise = QuestionExercise.Create(
            TestId.Create(request.TestId),
            request.Title,
            request.Description,
            difficultyEnum,
            false);

        AddQuestionChoicesToExercise(request, questionExercise);

        questionExercise.DetermineMultipleAnswers();

        foreach (var topicId in request.ExerciseTopics) // Add Topics
        {
            var topic = await _context.ExerciseTopics
                .FirstOrDefaultAsync(x => x.Id == ExerciseTopicId.Create(topicId), cancellationToken);

            if (topic is null)
            {
                return new NotFound();
            }

            questionExercise.AddTopic(topic);
        }

        _context.QuestionExercises.Add(questionExercise);

        await _context.SaveChangesAsync(cancellationToken);

        return questionExercise.Id.Value;
    }

    private static List<ValidationFailure> ValidateEnum(CreateQuestionExerciseCommand request)
    {
        return [new(nameof(request.Difficulty), $"Invalid difficulty level: {request.Difficulty}.")];
    }

    private static void AddQuestionChoicesToExercise(CreateQuestionExerciseCommand request, QuestionExercise questionExercise)
    {
        foreach (var answerDto in request.Answers)
        {
            var questionChoice = QuestionChoice.Create(questionExercise.Id, answerDto.Text, answerDto.IsCorrect);
            questionExercise.AddQuestionChoice(questionChoice);
        }
    }
}