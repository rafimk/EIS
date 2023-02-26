using EIS.Shared.Abstractions;
using EIS.Shared.Attributes;
using EIS.Shared.Contexts;
using EIS.Shared.Handlers;
using EIS.Shared.Messaging.Exceptions;

namespace EIS.Shared.RabbitMQ.Exceptions;

[Decorator]
internal sealed class MessagingErrorHandlingEventHandlerDecorator<T> : IEventHandler<T> where T : class, IEvent
{
    private readonly IEventHandler<T> _handler;
    private readonly IContextProvider _contextProvider;
    private readonly IMessagingExceptionPolicyHandler _messagingExceptionPolicyHandler;

    public MessagingErrorHandlingEventHandlerDecorator(IEventHandler<T> handler, IContextProvider contextProvider,
        IMessagingExceptionPolicyHandler messagingExceptionPolicyHandler)
    {
        _handler = handler;
        _contextProvider = contextProvider;
        _messagingExceptionPolicyHandler = messagingExceptionPolicyHandler;
    }

    public Task HandleAsync(T @event, CancellationToken cancellationToken = default)
    {
        var context = _contextProvider.Current();
        if (string.IsNullOrWhiteSpace(context.MessageId))
        {
            return _handler.HandleAsync(@event, cancellationToken);
        }

        return _messagingExceptionPolicyHandler.HandleAsync(@event,
            () => _handler.HandleAsync(@event, cancellationToken));
    }
}