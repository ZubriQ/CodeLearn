namespace CodeLearn.Domain.Common;

public abstract class BaseAuditableEntity<TId> : BaseEntity<TId>
{
    protected BaseAuditableEntity()
    {
    }

    protected BaseAuditableEntity(TId id) : base(id)
    {
    }

    public DateTimeOffset Created { get; set; }

    public string? CreatedBy { get; set; }

    public DateTimeOffset LastModified { get; set; }

    public string? LastModifiedBy { get; set; }
}