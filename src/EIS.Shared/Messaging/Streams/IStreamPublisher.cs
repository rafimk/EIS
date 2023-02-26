using EIS.Shared.Abstractions;

namespace EIS.Shared.Messaging.Streams;

public interface IStreamPublisher
{
    Task PublishAsync<T>(string stream, T message, CancellationToken cancellationToken = default) where T : IMessage;
}