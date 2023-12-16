namespace CodeLearn.Domain.Exercises;

public sealed class MethodCodingExercise : Exercise
{
    public string MethodName { get; private set; }
    public string MethodStartingCode { get; private set; }
    public DataTypeId MethodReturnTypeId { get; private set; } = null!;
    public DataType MethodReturnType { get; private set; } = null!;

    private readonly IList<MethodParameter> _methodParameters = [];
    public IReadOnlyList<MethodParameter> MethodParameters => _methodParameters.AsReadOnly();

    private readonly IList<TestCase> _testCases = [];
    public IReadOnlyList<TestCase> TestCases => _testCases.AsReadOnly();

    private MethodCodingExercise(ExerciseId id,
        TestingId testingId,
        string title,
        string description,
        ExerciseDifficulty difficulty,
        string methodName,
        string methodStartingCode)
        : base(id, testingId, title, description, difficulty)
    {
        MethodName = methodName;
        MethodStartingCode = methodStartingCode;
    }

    public static MethodCodingExercise Create(
        TestingId testingId,
        string title,
        string description,
        ExerciseDifficulty difficulty,
        string methodName,
        string methodStartingCode,
        DataType dataType)
    {
        var exercise = new MethodCodingExercise(
           ExerciseId.CreateUnique(),
           testingId,
           title,
           description,
           difficulty,
           methodName,
           methodStartingCode
       )
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