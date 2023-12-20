namespace CodeLearn.Domain.TestingSessions.ValueObjects;

public sealed class TestingSessionId(Guid value) : ValueObject
{
    public Guid Value { get; } = value;

    public static TestingSessionId Create(Guid value)
    {
        return new TestingSessionId(value);
    }

    public static TestingSessionId CreateUnique()
    {
        return new TestingSessionId(Guid.NewGuid());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}