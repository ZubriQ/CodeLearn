namespace CodeLearn.Contracts.Exercises.Question;

public record QuestionExerciseResponse(
    int Id,
    int TestId,
    string Description,
    string Difficulty,
    QuestionChoiceResponseDto[] QuestionChoices);