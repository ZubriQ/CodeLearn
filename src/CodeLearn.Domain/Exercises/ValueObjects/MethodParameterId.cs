namespace CodeLearn.Domain.Exercises.ValueObjects;

public sealed class MethodParameterId(Guid value) : ValueObject
{
    public Guid Value { get; } = value;

    public static MethodParameterId Create(Guid value)
    {
        return new MethodParameterId(value);
    }

    public static MethodParameterId CreateUnique()
    {
        return new MethodParameterId(Guid.NewGuid());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}