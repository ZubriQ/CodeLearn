namespace CodeLearn.Contracts.Exercises.Question;

public record QuestionChoiceRequestDto(
    string Text,
    bool IsCorrect);