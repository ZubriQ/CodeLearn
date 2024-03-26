using CodeLearn.Contracts.Exercises.MethodCoding.Dto;

namespace CodeLearn.Contracts.Exercises.MethodCoding;

public record StudentMethodCodingExerciseResponse(
    int Id,
    string Title,
    string Description,
    string Difficulty,
    string MethodToExecute,
    string MethodSolutionCode,
    int MethodReturnTypeId,
    ExerciseTopicResponseDto[] ExerciseTopics,
    ExerciseNoteResponseDto[] ExerciseNotes,
    InputOutputExampleResponseDto[] InputOutputExamples,
    MethodParameterResponseDto[] MethodParameters);