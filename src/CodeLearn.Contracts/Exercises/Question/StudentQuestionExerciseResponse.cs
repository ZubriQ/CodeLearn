namespace CodeLearn.Contracts.Exercises.Question;

public record StudentQuestionChoiceResponseDto(
    int Id,
    string Text);

public record StudentQuestionExerciseResponse(
    int Id,
    int TestId,
    string Title,
    string Description,
    string Difficulty,
    bool IsMultipleAnswers,
    StudentQuestionChoiceResponseDto[] QuestionChoices);