namespace CodeLearn.Domain.Exercises.ValueObjects;

public sealed class ExerciseId(Guid value) : ValueObject
{
    public Guid Value { get; } = value;

    public static ExerciseId Create(Guid value)
    {
        return new ExerciseId(value);
    }

    public static ExerciseId CreateUnique()
    {
        return new ExerciseId(Guid.NewGuid());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}