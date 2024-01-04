namespace CodeLearn.Domain.Tests.ValueObjects;

public sealed class TestId(Guid value) : ValueObject
{
    public Guid Value { get; } = value;

    public static TestId Create(Guid value)
    {
        return new TestId(value);
    }

    public static TestId CreateUnique()
    {
        return new TestId(Guid.NewGuid());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}