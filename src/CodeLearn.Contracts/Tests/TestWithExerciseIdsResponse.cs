namespace CodeLearn.Contracts.Tests;

public record TestWithExerciseIdsResponse(
    int Id,
    string Title,
    string Description,
    int[] MethodCodingExercises,
    int[] QuestionExercises);