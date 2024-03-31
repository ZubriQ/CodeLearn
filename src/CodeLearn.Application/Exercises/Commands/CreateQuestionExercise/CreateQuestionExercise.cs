using CodeLearn.Domain.Exercises;
using CodeLearn.Domain.Exercises.Enums;
using CodeLearn.Domain.QuestionChoices;
using CodeLearn.Domain.Tests.ValueObjects;
using FluentValidation.Results;

namespace CodeLearn.Application.Exercises.Commands.CreateQuestionExercise;

public record AnswerDto(string Text, bool IsCorrect);

public record CreateQuestionExerciseCommand(
    int TestId,
    string Title,
    string Description,
    string Difficulty,
    List<AnswerDto> Answers)
    : IRequest<OneOf<int, ValidationFailed>>;

public class CreateQuestionExerciseCommandHandler(
    IApplicationDbContext _context,
    IValidator<CreateQuestionExerciseCommand> _validator)
    : IRequestHandler<CreateQuestionExerciseCommand, OneOf<int, ValidationFailed>>
{
    public async Task<OneOf<int, ValidationFailed>> Handle(CreateQuestionExerciseCommand request, CancellationToken cancellationToken)
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
            true); // TODO: handle multiple answers

        AddQuestionChoicesToExercise(request, questionExercise);

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