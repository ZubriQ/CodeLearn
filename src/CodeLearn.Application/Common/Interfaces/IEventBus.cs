namespace CodeLearn.Application.Common.Interfaces;

public interface IEventBus
{
    Task PublicAsync<T>(T message, CancellationToken cancellationToken = default)
        where T : class;
}