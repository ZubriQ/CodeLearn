namespace CodeLearn.Domain.Teachers.ValueObjects;

public sealed class TeacherId(Guid value) : ValueObject
{
    public Guid Value { get; private set; } = value;

    public static TeacherId Create(Guid value)
    {
        return new(value);
    }

    public static TeacherId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}