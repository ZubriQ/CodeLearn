namespace CodeLearn.Domain.Exercises.ValueObjects;

public sealed class TestCaseParameterId(Guid value) : ValueObject
{
    public Guid Value { get; } = value;

    public static TestCaseParameterId Create(Guid value)
    {
        return new TestCaseParameterId(value);
    }

    public static TestCaseParameterId CreateUnique()
    {
        return new TestCaseParameterId(Guid.NewGuid());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}