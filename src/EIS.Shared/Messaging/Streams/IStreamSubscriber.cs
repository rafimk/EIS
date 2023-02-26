using EIS.Shared.Abstractions;

namespace EIS.Shared.Messaging.Streams;

public interface IStreamSubscriber
{
    Task SubscribeAsync<T>(string stream, Func<T, Task> handler, CancellationToken cancellationToken = default)
        where T : class, IMessage;
}