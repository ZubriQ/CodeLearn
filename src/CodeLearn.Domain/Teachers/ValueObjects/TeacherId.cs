namespace CodeLearn.Domain.Teachers.ValueObjects;

public sealed class TeacherId(Guid value) : ValueObject
{
    public Guid Value { get; } = value;

    public static TeacherId Create(Guid value)
    {
        return new TeacherId(value);
    }

    public static TeacherId CreateUnique()
    {
        return new TeacherId(Guid.NewGuid());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}