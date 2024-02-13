namespace CodeLearn.Domain.QuestionChoices.ValueObjects;

public sealed class QuestionChoiceId(int value) : ValueObject
{
    public int Value { get; } = value;

    public static QuestionChoiceId Create(int value)
    {
        return new QuestionChoiceId(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}