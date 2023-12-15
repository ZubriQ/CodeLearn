namespace CodeLearn.Domain.Exercises.ValueObjects;

public sealed class ExerciseNoteId(Guid value) : ValueObject
{
    public Guid Value { get; } = value;

    public static ExerciseNoteId Create(Guid value)
    {
        return new ExerciseNoteId(value);
    }

    public static ExerciseNoteId CreateUnique()
    {
        return new ExerciseNoteId(Guid.NewGuid());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}