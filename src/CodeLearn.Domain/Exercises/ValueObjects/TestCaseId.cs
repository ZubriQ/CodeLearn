namespace CodeLearn.Domain.Exercises.ValueObjects;

public sealed class TestCaseId(Guid value) : ValueObject
{
    public Guid Value { get; } = value;

    public static TestCaseId Create(Guid value)
    {
        return new TestCaseId(value);
    }

    public static TestCaseId CreateUnique()
    {
        return new TestCaseId(Guid.NewGuid());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}