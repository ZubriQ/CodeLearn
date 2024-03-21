namespace CodeLearn.Domain.Exercises.ValueObjects;

public sealed class MethodParameterId(int value) : ValueObject
{
    public int Value { get; } = value;

    public static MethodParameterId Create(int value)
    {
        return new MethodParameterId(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}