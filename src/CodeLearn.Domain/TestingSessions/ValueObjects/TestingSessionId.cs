namespace CodeLearn.Domain.TestingSessions.ValueObjects;

public sealed class TestingSessionId(int value) : ValueObject
{
    public int Value { get; } = value;

    public static TestingSessionId Create(int value)
    {
        return new TestingSessionId(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}