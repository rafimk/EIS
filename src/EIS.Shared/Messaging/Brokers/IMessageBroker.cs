using EIS.Shared.Abstractions;

namespace EIS.Shared.Messaging.Brokers;

public interface IMessageBroker
{
    Task SendAsync<T>(T message, CancellationToken cancellationToken = default) where T : IMessage;
}