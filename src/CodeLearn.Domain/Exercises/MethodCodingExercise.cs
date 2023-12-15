namespace CodeLearn.Domain.Exercises;

public sealed class MethodCodingExercise(
    ExerciseId id,
    TestingId testingId,
    string title,
    string description,
    ExerciseDifficulty difficulty)
    : Exercise(id, testingId, title, description, difficulty)
{
    public string MethodName { get; private set; } = null!;
    public DataTypeId MethodReturnTypeId { get; private set; } = null!;
    public DataType MethodReturnType { get; private set; } = null!;
}