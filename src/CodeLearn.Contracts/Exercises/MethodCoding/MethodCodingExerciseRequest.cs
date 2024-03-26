namespace CodeLearn.Contracts.Exercises.MethodCoding;

public record ExerciseNoteDto(string Entry, string Decoration);

public record InputOutputExampleDto(string Input, string Output);

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
    ExerciseNoteDto[] ExerciseNotes,
    InputOutputExampleDto[] InputOutputExamples,
    MethodParameterRequestDto[] MethodParameters,
    TestCaseRequestDto[] TestCases);