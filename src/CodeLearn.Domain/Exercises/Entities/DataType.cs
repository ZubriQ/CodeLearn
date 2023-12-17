namespace CodeLearn.Domain.Exercises.Entities;

public sealed class DataType : BaseEntity<DataTypeId>
{
    public string SystemName { get; private set; } = null!;
    public string Alias { get; private set; } = null!;

    private DataType() { }

    private DataType(DataTypeId id, string systemName, string alias)
        : base(id)
    {
        SystemName = systemName;
        Alias = alias;
    }

    public static DataType Create(string systemName, string alias)
    {
        return new DataType(
            DataTypeId.Create(Guid.NewGuid()),
            systemName,
            alias);
    }
}