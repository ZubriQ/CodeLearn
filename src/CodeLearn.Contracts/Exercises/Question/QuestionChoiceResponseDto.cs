namespace CodeLearn.Contracts.Exercises.Question;

public record QuestionChoiceResponseDto(
    int Id,
    string Text,
    bool IsCorrect);