namespace CodeLearn.Domain.Exercises.ValueObjects;

public sealed class ExerciseNoteId(int value) : ValueObject
{
    public int Value { get; private set; } = value;

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}