namespace CodeLearn.Domain.Exercises.ValueObjects;

public sealed class TestCaseId(int value) : ValueObject
{
    public int Value { get; } = value;

    public static TestCaseId Create(int value)
    {
        return new TestCaseId(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}