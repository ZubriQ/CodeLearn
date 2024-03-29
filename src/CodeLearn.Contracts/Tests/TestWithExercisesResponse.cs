namespace CodeLearn.Contracts.Tests;

public record ExerciseResponseDto(
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
    ExerciseResponseDto[] MethodCodingExercises,
    ExerciseResponseDto[] QuestionExercises);