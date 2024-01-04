namespace CodeLearn.Domain.Exercises;

public sealed class MethodCodingExercise : Exercise
{
    public string MethodToExecute { get; private set; }
    public string MethodSolutionCode { get; private set; }
    public DataTypeId MethodReturnTypeId { get; private set; } = null!;
    public DataType MethodReturnType { get; private set; } = null!;

    private readonly IList<MethodParameter> _methodParameters = [];
    public IReadOnlyList<MethodParameter> MethodParameters => _methodParameters.AsReadOnly();

    private readonly IList<TestCase> _testCases = [];
    public IReadOnlyList<TestCase> TestCases => _testCases.AsReadOnly();

    private readonly IList<InputOutputExample> _inputOutputExamples = [];
    public IReadOnlyList<InputOutputExample> InputOutputExamples => _inputOutputExamples.AsReadOnly();

    private MethodCodingExercise(
        ExerciseId id,
        TestId testId,
        string title,
        string description,
        ExerciseDifficulty difficulty,
        string methodToExecute,
        string methodSolutionCode)
        : base(id, testId, title, description, difficulty)
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
           ExerciseId.CreateUnique(),
           testId,
           title,
           description,
           difficulty,
           methodToExecute,
           methodSolutionCode)
        {
            MethodReturnType = dataType,
            MethodReturnTypeId = dataType.Id
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
}