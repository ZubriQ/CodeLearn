namespace CodeLearn.Domain.Exercises.Entities;

public sealed class InputOutputExample : BaseEntity<InputOutputExampleId>
{
    public ExerciseId ExerciseId { get; private set; } = null!;
    public string Input { get; private set; } = null!;
    public string Output { get; private set; } = null!;

    private InputOutputExample() { }

    private InputOutputExample(
        ExerciseId exerciseId,
        string input,
        string output)
        : base(default!)
    {
        Input = input;
        Output = output;
        ExerciseId = exerciseId;
    }

    public static InputOutputExample Create(
        ExerciseId exerciseId,
        string input,
        string output)
    {
        return new InputOutputExample(
            exerciseId,
            input,
            output);
    }
}