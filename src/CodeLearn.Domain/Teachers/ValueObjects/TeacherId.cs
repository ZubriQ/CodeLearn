namespace CodeLearn.Domain.Teachers.ValueObjects;

public sealed class TeacherId(int value) : ValueObject
{
    public int Value { get; private set; } = value;

    public static TeacherId Create(int value)
    {
        return new(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}