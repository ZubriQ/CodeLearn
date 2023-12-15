using System.ComponentModel.DataAnnotations.Schema;

namespace CodeLearn.Domain.Common;

public abstract class BaseEntity<TId>
{
    public TId Id { get; }

    private readonly List<BaseEvent> _domainEvents = [];

    [NotMapped]
    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected BaseEntity(TId id)
    {
        Id = id;
    }

    public void AddDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    #region Equality methods

    public static bool operator ==(BaseEntity<TId>? first, BaseEntity<TId>? second)
    {
        if (ReferenceEquals(first, second))
        {
            return true;
        }

        if (first is null || second is null)
        {
            return false;
        }

        return first.Equals(second);
    }

    public static bool operator !=(BaseEntity<TId>? first, BaseEntity<TId>? second)
    {
        return !(first == second);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as BaseEntity<TId>);
    }

    private bool Equals(BaseEntity<TId>? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        if (other.GetType() != GetType())
        {
            return false;
        }

        return EqualityComparer<TId>.Default.Equals(other.Id, Id);
    }

    public override int GetHashCode()
    {
        return Id?.GetHashCode() ?? 0;
    }

    #endregion

#pragma warning disable CS8618
    protected BaseEntity() { }
#pragma warning restore CS8618 
}