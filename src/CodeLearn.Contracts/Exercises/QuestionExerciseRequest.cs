namespace CodeLearn.Contracts.QuestionExercises;

public record QuestionExerciseRequest(
    string Title,
    string Description,
    string Difficulty,
    bool IsMultipleAnswers,
    QuestionChoiceRequestDto[] Answers);