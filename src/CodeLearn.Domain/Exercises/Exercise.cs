using System.Collections.Generic;

namespace CodeLearn.Domain.Exercises;

public abstract class Exercise : BaseEntity<ExerciseId>, IAggregateRoot
{
    public TestingId TestingId { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public ExerciseDifficulty Difficulty { get; private set; }

    private readonly IList<ExerciseNote> _exerciseNotes = [];
    public IReadOnlyList<ExerciseNote> ExerciseNotes => _exerciseNotes.AsReadOnly();

    private readonly List<ExerciseTopic> _exerciseTopics = [];
    public IEnumerable<ExerciseTopic> ExerciseTopics => _exerciseTopics.AsReadOnly();

    protected Exercise(
        ExerciseId id,
        TestingId testingId,
        string title,
        string description,
        ExerciseDifficulty difficulty)
        : base(id)
    {
        TestingId = testingId;
        Title = title;
        Description = description;
        Difficulty = difficulty;
    }

    public void AddExerciseTopic(ExerciseTopic exerciseTopic)
    {
        _exerciseTopics.Add(exerciseTopic);
        exerciseTopic.AddExercise(this);
    }
}