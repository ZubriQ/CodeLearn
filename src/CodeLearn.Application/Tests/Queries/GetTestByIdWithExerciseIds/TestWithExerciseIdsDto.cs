namespace CodeLearn.Application.Tests.Queries.GetTestByIdWithExerciseIds;

public record TestWithExerciseIdsDto(
    int Id,
    string Title,
    string Description,
    int[] MethodCodingExercises,
    int[] QuestionExercises);