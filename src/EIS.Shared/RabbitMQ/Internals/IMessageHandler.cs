using EIS.Shared.Abstractions;

namespace EIS.Shared.RabbitMQ.Internals;

public interface IMessageHandler
{
    Task HandleAsync<T>(Func<IServiceProvider, T, CancellationToken, Task> handler, T message,
        CancellationToken cancellationToken = default) where T : IMessage;
}