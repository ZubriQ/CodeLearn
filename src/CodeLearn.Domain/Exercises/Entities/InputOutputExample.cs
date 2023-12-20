namespace CodeLearn.Domain.Exercises.Entities;

public sealed class InputOutputExample : BaseEntity<InputOutputExampleId>
{
    public ExerciseId ExerciseId { get; set; } = null!;
    public string Input { get; set; } = null!;
    public string Output { get; set; } = null!;
    public string? Explanation { get; set; }

    private InputOutputExample() { }

    private InputOutputExample(
        InputOutputExampleId id,
        ExerciseId exerciseId,
        string input,
        string output,
        string? explanation)
        : base(id)
    {
        Input = input;
        Output = output;
        Explanation = explanation;
        ExerciseId = exerciseId;
    }

    public static InputOutputExample Create(
        ExerciseId exerciseId,
        string input,
        string output,
        string? explanation = null)
    {
        return new InputOutputExample(
            InputOutputExampleId.CreateUnique(),
            exerciseId,
            input,
            output,
            explanation);
    }
}