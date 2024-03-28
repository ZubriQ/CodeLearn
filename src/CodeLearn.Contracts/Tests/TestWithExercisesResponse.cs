namespace CodeLearn.Contracts.Tests;

public record ExerciseDto(
    int Id,
    string Title,
    string Description,
    string Difficulty);

public record TestWithExercisesResponse(
    int Id,
    string Title,
    string Description,
    bool IsPublic,
    DateTimeOffset Created,
    DateTimeOffset LastModified,
    ExerciseDto[] MethodCodingExercises,
    ExerciseDto[] QuestionExercises);