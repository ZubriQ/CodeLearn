namespace CodeLearn.Domain.Exercises.ValueObjects;

public sealed class ExerciseNoteId(Guid value) : ValueObject
{
    public Guid Value { get; private set; } = value;

    public static ExerciseNoteId Create(Guid value)
    {
        return new(value);
    }

    public static ExerciseNoteId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}