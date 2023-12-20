namespace CodeLearn.Domain.Exercises.ValueObjects;

public sealed class InputOutputExampleId(Guid value) : ValueObject
{
    public Guid Value { get; } = value;

    public static InputOutputExampleId Create(Guid value)
    {
        return new InputOutputExampleId(value);
    }

    public static InputOutputExampleId CreateUnique()
    {
        return new InputOutputExampleId(Guid.NewGuid());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}