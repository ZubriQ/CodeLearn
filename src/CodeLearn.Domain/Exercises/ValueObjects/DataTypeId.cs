namespace CodeLearn.Domain.Exercises.ValueObjects;

public sealed class DataTypeId(int value) : ValueObject
{
    public int Value { get; } = value;

    public static DataTypeId Create(int value)
    {
        return new DataTypeId(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}