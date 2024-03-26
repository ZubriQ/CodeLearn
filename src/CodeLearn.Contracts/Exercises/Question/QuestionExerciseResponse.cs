namespace CodeLearn.Contracts.Exercises.Question;

public record QuestionChoiceResponseDto(
    int Id,
    string Text,
    bool IsCorrect);

public record QuestionExerciseResponse(
    int Id,
    int TestId,
    string Description,
    string Difficulty,
    bool IsMultipleAnswers,
    QuestionChoiceResponseDto[] QuestionChoices);