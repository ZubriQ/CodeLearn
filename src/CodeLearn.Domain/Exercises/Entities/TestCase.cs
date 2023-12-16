namespace CodeLearn.Domain.Exercises.Entities;

public sealed class TestCase : BaseEntity<TestCaseId>
{
    public ExerciseId ExerciseId { get; private set; } = null!;
    public string CorrectOutputValue { get; private set; } = null!;

    private readonly IList<TestCaseParameter> _testCaseParameters = [];
    public IReadOnlyList<TestCaseParameter> TestCaseParameters => _testCaseParameters.AsReadOnly();
    
    private TestCase() { }
    
    private TestCase(TestCaseId id, ExerciseId exerciseId, string correctOutputValue) 
        : base(id)
    {
        ExerciseId = exerciseId;
        CorrectOutputValue = correctOutputValue;
    }

    public static TestCase Create(ExerciseId exerciseId, string correctOutputValue)
    {
        return new TestCase(TestCaseId.CreateUnique(), exerciseId, correctOutputValue);
    }
    
    public void AddTestCaseParameter(TestCaseParameter testCaseParameter)
    {
        _testCaseParameters.Add(testCaseParameter);
    }
}