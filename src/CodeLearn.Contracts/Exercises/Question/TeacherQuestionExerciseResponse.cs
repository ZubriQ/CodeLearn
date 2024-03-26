namespace CodeLearn.Contracts.Exercises.Question;

public record TeacherQuestionChoiceResponseDto(
    int Id,
    string Text,
    bool IsCorrect);

public record TeacherQuestionExerciseResponse(
    int Id,
    int TestId,
    string Title,
    string Description,
    string Difficulty,
    bool IsMultipleAnswers,
    TeacherQuestionChoiceResponseDto[] QuestionChoices);