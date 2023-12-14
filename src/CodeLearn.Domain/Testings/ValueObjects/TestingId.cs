namespace CodeLearn.Domain.Testings.ValueObjects;

public sealed class TestingId(Guid value) : ValueObject
{
    public Guid Value { get; private set; } = value;

    public static TestingId Create(Guid value)
    {
        return new(value);
    }

    public static TestingId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}