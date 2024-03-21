namespace CodeLearn.Domain.Exercises.Entities;

public sealed class MethodParameter : BaseEntity<MethodParameterId>
{
    public ExerciseId ExerciseId { get; private set; } = null!;
    public DataTypeId DataTypeId { get; private set; } = null!;
    public DataType DataType { get; private set; } = null!;
    public int Position { get; private set; }

    private MethodParameter() { }

    private MethodParameter(ExerciseId exerciseId, DataType dataType, int position)
        : base(default!)
    {
        ExerciseId = exerciseId;
        DataTypeId = dataType.Id;
        DataType = dataType;
        Position = position;
    }

    public static MethodParameter Create(ExerciseId exerciseId, DataType dataType, int position)
    {
        return new MethodParameter(
            exerciseId,
            dataType,
            position
        );
    }
}