namespace CodeLearn.Domain.ExerciseTopics.ValueObjects;

public sealed class ExerciseTopicId(int value) : ValueObject
{
    public int Value { get; } = value;

    public static ExerciseTopicId Create(int value)
    {
        return new ExerciseTopicId(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}