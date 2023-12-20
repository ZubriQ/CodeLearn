﻿namespace CodeLearn.Domain.Exercises.Entities;

public sealed class ExerciseTopic : BaseEntity<ExerciseTopicId> // TODO: Aggregate root? Just Read with CQRS?
{
    public string Name { get; private set; } = null!;

    private readonly IList<Exercise> _exercises = [];
    public IReadOnlyList<Exercise> Exercises => _exercises.AsReadOnly();

    private ExerciseTopic() { }

    private ExerciseTopic(ExerciseTopicId exerciseTopicId, string name)
        : base(exerciseTopicId)
    {
        Name = name;
    }

    public static ExerciseTopic Create(string name)
    {
        return new ExerciseTopic(ExerciseTopicId.CreateUnique(), name);
    }

    internal void AddExercise(Exercise exercise)
    {
        _exercises.Add(exercise);
    }
}