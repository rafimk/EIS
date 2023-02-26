using EIS.Shared.Abstractions;

namespace EIS.Shared.Messaging.Streams;

internal sealed class DefaultStreamSubscriber : IStreamSubscriber
{
    public Task SubscribeAsync<T>(string stream, Func<T, Task> handler, CancellationToken cancellationToken = default)
        where T : class, IMessage => Task.CompletedTask;
}