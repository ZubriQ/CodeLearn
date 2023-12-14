namespace CodeLearn.Domain.Exercises;

public class Exercise : BaseEntity<ExerciseId>, IAggregateRoot
{
    private readonly List<ExerciseNote> _exerciseNotes = [];

    public TestingId TestingId { get; private set; } = null!;
    public string Title { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public ExerciseDifficulty Difficulty { get; private set; }

    public IReadOnlyList<ExerciseNote> ExerciseNotes => _exerciseNotes.AsReadOnly();

    protected Exercise(
        TestingId testingId,
        string title,
        string description,
        ExerciseDifficulty difficulty)
    {
        TestingId = testingId;
        Title = title;
        Description = description;
        Difficulty = difficulty;
    }
}