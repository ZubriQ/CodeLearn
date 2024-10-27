using CodeLearn.Application.Common.Interfaces;
using MassTransit;

namespace CodeLearn.Infrastructure.Utilities;

/// <summary>
/// Sends messages to RabbitMQ.
/// </summary>
public sealed class EventBus(IPublishEndpoint publishEndpoint) : IEventBus
{
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

    /// <summary>
    /// Send message to RabbitMQ.
    /// </summary>
    /// <typeparam name="T">For example, EntityCreatedEvent class.</typeparam>
    /// <param name="message">DTO.</param>
    /// <param name="cancellationToken">Token.</param>
    public Task PublicAsync<T>(T message, CancellationToken cancellationToken = default)
        where T : class => 
            _publishEndpoint.Publish<T>(message, cancellationToken);
}