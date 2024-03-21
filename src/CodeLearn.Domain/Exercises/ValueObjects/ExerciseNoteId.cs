namespace CodeLearn.Domain.Exercises.ValueObjects;

public sealed class ExerciseNoteId(int value) : ValueObject
{
    public int Value { get; } = value;

    public static ExerciseNoteId Create(int value)
    {
        return new ExerciseNoteId(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}