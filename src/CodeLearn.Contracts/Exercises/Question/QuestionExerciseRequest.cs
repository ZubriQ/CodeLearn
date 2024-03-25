namespace CodeLearn.Contracts.Exercises.Question;

public record QuestionExerciseRequest(
    string Title,
    string Description,
    string Difficulty,
    bool IsMultipleAnswers,
    QuestionChoiceRequestDto[] Answers);