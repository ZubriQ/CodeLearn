namespace CodeLearn.Domain.Testings.ValueObjects;

public sealed class TestingId(Guid value) : ValueObject
{
    public Guid Value { get; } = value;

    public static TestingId Create(Guid value)
    {
        return new TestingId(value);
    }

    public static TestingId CreateUnique()
    {
        return new TestingId(Guid.NewGuid());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}