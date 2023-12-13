namespace CodeLearn.Domain.Testings.ValueObjects;

public sealed class TestingId(int value) : ValueObject
{
    public int Value { get; private set; } = value;

    public static TestingId Create(int value)
    {
        return new(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}