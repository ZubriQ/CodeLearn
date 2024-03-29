namespace CodeLearn.Contracts.Exercises.MethodCoding;

public record ExerciseNoteRequestDto(string Entry, string Decoration);

public record InputOutputExampleRequestDto(string Input, string Output);

public record MethodParameterRequestDto(int DataTypeId, int Position);

public record TestCaseParameterRequestDto(string Value, int Position);

public record TestCaseRequestDto(
    string CorrectOutputValue,
    TestCaseParameterRequestDto[] TestCaseParameters);

public record MethodCodingExerciseRequest(
    string Title,
    string Description,
    string Difficulty,
    int[] ExerciseTopics,
    string MethodToExecute,
    string MethodSolutionCode,
    int MethodReturnTypeId,
    ExerciseNoteRequestDto[] ExerciseNotes,
    InputOutputExampleRequestDto[] InputOutputExamples,
    MethodParameterRequestDto[] MethodParameters,
    TestCaseRequestDto[] TestCases);