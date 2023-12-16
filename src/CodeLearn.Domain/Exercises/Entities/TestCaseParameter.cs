namespace CodeLearn.Domain.Exercises.Entities;

public sealed class TestCaseParameter : BaseEntity<TestCaseParameterId>
{
    public TestCaseId TestCaseId { get; private set; } = null!;
    public string Value { get; private set; } = null!;
    public int Position { get; private set; }
    
    private TestCaseParameter () { }

    private TestCaseParameter(TestCaseParameterId id, TestCaseId testCaseId, string value, int position) 
        : base(id)
    {
        TestCaseId = testCaseId;
        Value = value;
        Position = position;
    }

    public static TestCaseParameter Create(TestCaseId testCaseId, string value, int position)
    {
        return new TestCaseParameter(TestCaseParameterId.CreateUnique(), testCaseId, value, position);
    }
}