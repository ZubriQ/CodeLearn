namespace CodeLearn.Domain.Exercises.ValueObjects;

public sealed class DataTypeId(Guid value) : ValueObject
{
    public Guid Value { get; } = value;

    public static DataTypeId Create(Guid value)
    {
        return new DataTypeId(value);
    }

    public static DataTypeId CreateUnique()
    {
        return new DataTypeId(Guid.NewGuid());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}