namespace CodeLearn.Domain.ExerciseSubmissions.ValueObjects;

public sealed class ExerciseSubmissionId(long value) : ValueObject
{
    public long Value { get; } = value;

    public static ExerciseSubmissionId Create(long value)
    {
        return new ExerciseSubmissionId(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}