namespace CodeLearn.Domain.ExerciseTopics;

public sealed class ExerciseTopic : BaseAuditableEntity<ExerciseTopicId>, IAggregateRoot
{
    public string Name { get; private set; }

    private readonly IList<Exercise> _exercises = [];
    public IReadOnlyList<Exercise> Exercises => _exercises.AsReadOnly();

    private ExerciseTopic(string name)
        : base(default!)
    {
        Name = name;
    }

    public static ExerciseTopic Create(string name)
    {
        return new ExerciseTopic(name);
    }

    internal void AddExercise(Exercise exercise)
    {
        _exercises.Add(exercise);
    }
}