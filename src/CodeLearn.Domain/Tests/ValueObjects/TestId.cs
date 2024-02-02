namespace CodeLearn.Domain.Tests.ValueObjects;

public sealed class TestId(int value) : ValueObject
{
    public int Value { get; } = value;

    public static TestId Create(int value)
    {
        return new TestId(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}