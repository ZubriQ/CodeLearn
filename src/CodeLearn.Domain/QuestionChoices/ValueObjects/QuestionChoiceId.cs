namespace CodeLearn.Domain.QuestionChoices.ValueObjects;

public sealed class QuestionChoiceId(Guid value) : ValueObject
{
    public Guid Value { get; } = value;

    public static QuestionChoiceId Create(Guid value)
    {
        return new QuestionChoiceId(value);
    }

    public static QuestionChoiceId CreateUnique()
    {
        return new QuestionChoiceId(Guid.NewGuid());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}