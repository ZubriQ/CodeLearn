namespace CodeLearn.Domain.ExerciseSubmissions.ValueObjects;

public sealed class ExerciseSubmissionId(Guid value) : ValueObject
{
    public Guid Value { get; } = value;

    public static ExerciseSubmissionId Create(Guid value)
    {
        return new ExerciseSubmissionId(value);
    }

    public static ExerciseSubmissionId CreateUnique()
    {
        return new ExerciseSubmissionId(Guid.NewGuid());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}