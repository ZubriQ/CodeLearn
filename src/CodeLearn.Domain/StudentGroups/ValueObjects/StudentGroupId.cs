namespace CodeLearn.Domain.StudentGroups.ValueObjects;

public sealed class StudentGroupId(int value) : ValueObject
{
    public int Value { get; } = value;

    public static StudentGroupId Create(int value)
    {
        return new StudentGroupId(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}