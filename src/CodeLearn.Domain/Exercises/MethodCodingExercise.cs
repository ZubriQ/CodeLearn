namespace CodeLearn.Domain.Exercises;

public sealed class MethodCodingExercise : Exercise
{
    public string MethodToExecute { get; private set; }
    public string MethodSolutionCode { get; private set; }
    public DataTypeId MethodReturnDataTypeId { get; private set; } = null!;
    public DataType MethodReturnDataType { get; private set; } = null!;

    private readonly IList<MethodParameter> _methodParameters = [];
    public IReadOnlyList<MethodParameter> MethodParameters => _methodParameters.AsReadOnly();

    private readonly IList<TestCase> _testCases = [];
    public IReadOnlyList<TestCase> TestCases => _testCases.AsReadOnly();

    private readonly IList<InputOutputExample> _inputOutputExamples = [];
    public IReadOnlyList<InputOutputExample> InputOutputExamples => _inputOutputExamples.AsReadOnly();

    private MethodCodingExercise(
        TestId testId,
        string title,
        string description,
        ExerciseDifficulty difficulty,
        string methodToExecute,
        string methodSolutionCode)
        : base(testId, title, description, difficulty)
    {
        MethodToExecute = methodToExecute;
        MethodSolutionCode = methodSolutionCode;
    }

    public static MethodCodingExercise Create(
        TestId testId,
        string title,
        string description,
        ExerciseDifficulty difficulty,
        string methodToExecute,
        string methodSolutionCode,
        DataType dataType)
    {
        var exercise = new MethodCodingExercise(
           testId,
           title,
           description,
           difficulty,
           methodToExecute,
           methodSolutionCode)
        {
            MethodReturnDataType = dataType,
            MethodReturnDataTypeId = dataType.Id
        };

        return exercise;
    }

    public void AddMethodParameter(MethodParameter methodParameter)
    {
        _methodParameters.Add(methodParameter);
    }

    public void AddTestCase(TestCase testCase)
    {
        _testCases.Add(testCase);
    }

    public void AddExample(InputOutputExample example)
    {
        _inputOutputExamples.Add(example);
    }
}