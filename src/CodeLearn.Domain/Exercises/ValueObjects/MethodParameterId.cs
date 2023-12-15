namespace CodeLearn.Domain.Exercises.ValueObjects;

public sealed class TestMethodParameterId(Guid value) : ValueObject
{
    public Guid Value { get; } = value;

    public static TestMethodParameterId Create(Guid value)
    {
        return new TestMethodParameterId(value);
    }

    public static TestMethodParameterId CreateUnique()
    {
        return new TestMethodParameterId(Guid.NewGuid());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}