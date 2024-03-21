namespace CodeLearn.Domain.Exercises.ValueObjects;

public sealed class TestCaseParameterId(int value) : ValueObject
{
    public int Value { get; } = value;

    public static TestCaseParameterId Create(int value)
    {
        return new TestCaseParameterId(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}