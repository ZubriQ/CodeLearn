namespace CodeLearn.Domain.Exercises.ValueObjects;

public sealed class ExerciseTopicId(Guid value) : ValueObject
{
    public Guid Value { get; } = value;

    public static ExerciseTopicId Create(Guid value)
    {
        return new ExerciseTopicId(value);
    }

    public static ExerciseTopicId CreateUnique()
    {
        return new ExerciseTopicId(Guid.NewGuid());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}