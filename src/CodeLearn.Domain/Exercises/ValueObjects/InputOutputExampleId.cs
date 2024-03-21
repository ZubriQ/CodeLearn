namespace CodeLearn.Domain.Exercises.ValueObjects;

public sealed class InputOutputExampleId(int value) : ValueObject
{
    public int Value { get; } = value;

    public static InputOutputExampleId Create(int value)
    {
        return new InputOutputExampleId(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}