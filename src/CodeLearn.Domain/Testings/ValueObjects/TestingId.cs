namespace CodeLearn.Domain.Testings.ValueObjects;

public sealed class TestingId(int value) : ValueObject
{
    public int Value { get; } = value;

    public static TestingId Create(int value)
    {
        return new TestingId(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}