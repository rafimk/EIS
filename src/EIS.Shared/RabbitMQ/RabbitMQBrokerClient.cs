using System.Collections.Concurrent;
using EasyNetQ;
using EIS.Shared.Contexts.Accessors;
using EIS.Shared.Messaging;
using EIS.Shared.Messaging.Clients;
using Humanizer;
using Microsoft.Extensions.Logging;
using IMessage = EIS.Shared.Abstractions.IMessage;

namespace EIS.Shared.RabbitMQ;

internal sealed class RabbitMqBrokerClient : IMessageBrokerClient
{
    private readonly ConcurrentDictionary<Type, string> _names = new();
    private readonly IBus _bus;
    private readonly IMessageContextAccessor _messageContextAccessor;
    private readonly ILogger<RabbitMqBrokerClient> _logger;

    public RabbitMqBrokerClient(IBus bus, IMessageContextAccessor messageContextAccessor,
        ILogger<RabbitMqBrokerClient> logger)
    {
        _bus = bus;
        _messageContextAccessor = messageContextAccessor;
        _logger = logger;
    }

    public async Task SendAsync<T>(MessageEnvelope<T> messageEnvelope, CancellationToken cancellationToken = default)
        where T : IMessage
    {
        var messageContext = messageEnvelope.Context;
        _messageContextAccessor.MessageContext = messageContext;
        var messageName = _names.GetOrAdd(typeof(T), typeof(T).Name.Underscore());
        _logger.LogInformation("Sending a message: {MessageName}  [ID: {MessageId}, Activity ID: {ActivityId}]...",
            messageName, messageContext.MessageId, messageContext.Context.ActivityId);
        await _bus.PubSub.PublishAsync(messageEnvelope.Message, cancellationToken);
    }
}