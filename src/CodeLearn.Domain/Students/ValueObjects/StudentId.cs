namespace CodeLearn.Domain.Students.ValueObjects;

public sealed class StudentId(Guid value) : ValueObject
{
    public Guid Value { get; } = value;

    public static StudentId Create(Guid value)
    {
        return new StudentId(value);
    }

    public static StudentId CreateUnique()
    {
        return new StudentId(Guid.NewGuid());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}