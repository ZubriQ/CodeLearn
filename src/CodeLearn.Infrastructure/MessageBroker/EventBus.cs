using CodeLearn.Application.Common.Interfaces;
using MassTransit;

namespace CodeLearn.Infrastructure.MessageBroker;

public sealed class EventBus(IPublishEndpoint publishEndpoint) : IEventBus
{
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

    public Task PublicAsync<T>(T message, CancellationToken cancellationToken = default)
        where T : class =>
        _publishEndpoint.Publish<T>(message, cancellationToken);
}