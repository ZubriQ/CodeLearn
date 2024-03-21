namespace CodeLearn.Domain.Exercises;

public sealed class QuestionExercise : Exercise
{
    public bool IsMultipleAnswers { get; private set; }

    private readonly IList<QuestionChoice> _questionChoices = [];
    public IReadOnlyList<QuestionChoice> QuestionChoices => _questionChoices.AsReadOnly();

    private QuestionExercise(
        TestId testId,
        string title,
        string description,
        ExerciseDifficulty difficulty,
        bool isMultipleAnswers)
        : base(testId, title, description, difficulty)
    {
        IsMultipleAnswers = isMultipleAnswers;
    }

    public static QuestionExercise Create(
        TestId testId,
        string title,
        string description,
        ExerciseDifficulty difficulty,
        bool isMultipleAnswers)
    {
        return new QuestionExercise(
            testId,
            title,
            description,
            difficulty,
            isMultipleAnswers);
    }

    public void AddQuestionChoice(QuestionChoice questionChoice)
    {
        _questionChoices.Add(questionChoice);
    }
}