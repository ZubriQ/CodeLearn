using CodeLearn.Contracts.Exercises.MethodCoding.Dto;

namespace CodeLearn.Contracts.Exercises.MethodCoding;

public record TestCaseParameterResponseDto(int Id, string Value, int Position);

public record TestCaseResponseDto(int Id, string CorrectOutputValue, TestCaseParameterResponseDto[] TestCaseParameters);

public record TeacherMethodCodingExerciseResponse(
    int Id,
    string Title,
    string Description,
    string Difficulty,
    string MethodToExecute,
    string MethodSolutionCode,
    int MethodReturnDataTypeId,
    ExerciseTopicResponseDto[] ExerciseTopics,
    ExerciseNoteResponseDto[] ExerciseNotes,
    InputOutputExampleResponseDto[] InputOutputExamples,
    MethodParameterResponseDto[] MethodParameters,
    TestCaseResponseDto[] TestCases);