namespace CodeLearn.Domain.Students.ValueObjects;

public sealed class StudentGroupId(Guid value) : ValueObject
{
    public Guid Value { get; } = value;

    public static StudentGroupId Create(Guid value)
    {
        return new StudentGroupId(value);
    }

    public static StudentGroupId CreateUnique()
    {
        return new StudentGroupId(Guid.NewGuid());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}