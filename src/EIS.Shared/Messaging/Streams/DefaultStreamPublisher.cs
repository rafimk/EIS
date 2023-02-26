using EIS.Shared.Abstractions;

namespace EIS.Shared.Messaging.Streams;

internal sealed class DefaultStreamPublisher : IStreamPublisher
{
    public Task PublishAsync<T>(string stream, T message, CancellationToken cancellationToken = default) where T : IMessage
        => Task.CompletedTask;
}