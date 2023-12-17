namespace CodeLearn.Domain.QuestionChoices;

public sealed class QuestionChoice : BaseEntity<QuestionChoiceId>, IAggregateRoot
{
    public ExerciseId ExerciseId { get; private set; } = null!;
    public string Text { get; private set; } = null!;
    public bool IsCorrect { get; private set; }
    public string? Explanation { get; private set; }

    private QuestionChoice() { }

    private QuestionChoice(
        QuestionChoiceId id,
        ExerciseId exerciseId,
        string text,
        bool isCorrect,
        string? explanation)
        : base(id)
    {
        ExerciseId = exerciseId;
        Text = text;
        IsCorrect = isCorrect;
        Explanation = explanation;
    }

    public static QuestionChoice Create(
        ExerciseId exerciseId,
        string text,
        bool isCorrect,
        string? explanation = null)
    {
        return new QuestionChoice(
            QuestionChoiceId.CreateUnique(),
            exerciseId,
            text,
            isCorrect,
            explanation);
    }
}