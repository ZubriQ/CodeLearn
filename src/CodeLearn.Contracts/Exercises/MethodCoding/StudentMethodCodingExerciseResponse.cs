using CodeLearn.Contracts.Exercises.MethodCoding.Dto;

namespace CodeLearn.Contracts.Exercises.MethodCoding;

public record StudentMethodCodingExerciseResponse(
    int Id,
    int TestId,
    string Title,
    string Description,
    string Difficulty,
    string MethodSolutionCode,
    ExerciseTopicResponseDto[] ExerciseTopics,
    ExerciseNoteResponseDto[] ExerciseNotes,
    InputOutputExampleResponseDto[] InputOutputExamples);