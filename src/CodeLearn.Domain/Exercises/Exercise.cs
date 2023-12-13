namespace CodeLearn.Domain.Exercises;

public sealed class Exercise : BaseEntity<ExerciseId>, IAggregateRoot
{
    public TestingId TestingId { get; private set; } = null!;

    public string Title { get; private set; } = null!;

    public string Description { get; private set; } = null!;

    public ExerciseDifficulty Difficulty { get; private set; }
}