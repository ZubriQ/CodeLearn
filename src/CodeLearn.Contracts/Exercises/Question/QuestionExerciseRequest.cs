namespace CodeLearn.Contracts.Exercises.Question;

public record QuestionChoiceRequestDto(
    string Text,
    bool IsCorrect);

public record QuestionExerciseRequest(
    string Title,
    string Description,
    string Difficulty,
    QuestionChoiceRequestDto[] Answers,
    int[] ExerciseTopics);