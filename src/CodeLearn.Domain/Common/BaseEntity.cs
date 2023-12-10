using System.ComponentModel.DataAnnotations.Schema;

namespace CodeLearn.Domain.Common;

public abstract class BaseEntity<TId>
{
    public TId Id { get; private set; }

    private readonly List<BaseEvent> _domainEvents = [];

    [NotMapped]
    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

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

    public bool Equals(BaseEntity<TId>? other)
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

    public override bool Equals(object? obj)
    {
        return Equals(obj as BaseEntity<TId>);
    }

    public override int GetHashCode()
    {
        return Id?.GetHashCode() ?? 0;
    }

    #endregion
}