namespace CodeLearn.Domain.Exercises.ValueObjects;

public sealed class ExerciseId(int value) : ValueObject
{
    public int Value { get; private set; } = value;

    public static ExerciseId Create(int value)
    {
        return new(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}