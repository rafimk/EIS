using EIS.Shared.Abstractions;

namespace EIS.Shared.Messaging.Clients;

public interface IMessageBrokerClient
{
    Task SendAsync<T>(MessageEnvelope<T> messageEnvelope, CancellationToken cancellationToken = default)
        where T : IMessage;
}