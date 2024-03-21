namespace CodeLearn.Domain.Exercises.ValueObjects;

public sealed class ExerciseId(int value) : ValueObject
{
    public int Value { get; } = value;

    public static ExerciseId Create(int value)
    {
        return new ExerciseId(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}