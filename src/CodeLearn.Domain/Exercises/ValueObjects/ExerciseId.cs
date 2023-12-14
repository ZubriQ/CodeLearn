namespace CodeLearn.Domain.Exercises.ValueObjects;

public sealed class ExerciseId(Guid value) : ValueObject
{
    public Guid Value { get; private set; } = value;

    public static ExerciseId Create(Guid value)
    {
        return new(value);
    }

    public static ExerciseId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}