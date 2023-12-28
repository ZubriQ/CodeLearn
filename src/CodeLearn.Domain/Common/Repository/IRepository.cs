namespace CodeLearn.Domain.Common.Repository;

public interface IRepository<T> where T : IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }
}