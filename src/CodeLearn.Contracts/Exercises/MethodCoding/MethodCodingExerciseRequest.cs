namespace CodeLearn.Contracts.Exercises.MethodCoding;

public record ExerciseNoteDto(string Entry, string Decoration);

public record InputOutputExampleDto(string Input, string Output);

public record MethodParameterDto(int DataTypeId, int Position);

public record TestCaseParameterDto(string Value, int Position);

public record TestCaseDto(string CorrectOutputValue, TestCaseParameterDto[] TestCaseParameters);

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
    MethodParameterDto[] MethodParameters,
    TestCaseDto[] TestCases);