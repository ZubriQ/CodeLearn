namespace CodeLearn.Domain.Exercises;

public abstract class Exercise(
    TestId testId,
    string title,
    string description,
    ExerciseDifficulty difficulty)
    : BaseEntity<ExerciseId>(default!), IAggregateRoot
{
    public TestId TestId { get; private set; } = testId;
    public string Title { get; private set; } = title;
    public string Description { get; private set; } = description;
    public ExerciseDifficulty Difficulty { get; private set; } = difficulty;

    private readonly IList<ExerciseTopic> _exerciseTopics = [];
    public virtual IReadOnlyList<ExerciseTopic> ExerciseTopics => _exerciseTopics.ToList();

    public void AddTopic(ExerciseTopic exerciseTopic)
    {
        _exerciseTopics.Add(exerciseTopic);
        exerciseTopic.AddExercise(this);
    }
}