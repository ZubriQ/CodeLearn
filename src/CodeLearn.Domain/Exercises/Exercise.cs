namespace CodeLearn.Domain.Exercises;

public abstract class Exercise(
    ExerciseId id,
    TestingId testingId,
    string title,
    string description,
    ExerciseDifficulty difficulty)
    : BaseEntity<ExerciseId>(id), IAggregateRoot
{
    public TestingId TestingId { get; private set; } = testingId;
    public string Title { get; private set; } = title;
    public string Description { get; private set; } = description;
    public ExerciseDifficulty Difficulty { get; private set; } = difficulty;

    private readonly IList<ExerciseNote> _exerciseNotes = [];
    public IReadOnlyList<ExerciseNote> ExerciseNotes => _exerciseNotes.AsReadOnly();

    private readonly List<ExerciseTopic> _exerciseTopics = [];
    public IEnumerable<ExerciseTopic> ExerciseTopics => _exerciseTopics.AsReadOnly();

    public void AddTopic(ExerciseTopic exerciseTopic)
    {
        _exerciseTopics.Add(exerciseTopic);
        exerciseTopic.AddExercise(this);
    }

    public void AddNote(ExerciseNote exerciseNote)
    {
        _exerciseNotes.Add(exerciseNote);
    }
}