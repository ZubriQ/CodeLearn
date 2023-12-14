
namespace CodeLearn.Domain.Exercises;

public sealed class MethodCodingExercise : Exercise
{
    public string MethodName { get; set; } = null!;

    public MethodCodingExercise(TestingId testingId, string title, string description, ExerciseDifficulty difficulty)
        : base(testingId, title, description, difficulty)
    {
    }
}