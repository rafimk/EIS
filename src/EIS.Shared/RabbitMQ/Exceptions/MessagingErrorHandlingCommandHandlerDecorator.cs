using EIS.Shared.Abstractions;
using EIS.Shared.Attributes;
using EIS.Shared.Contexts;
using EIS.Shared.Handlers;
using EIS.Shared.Messaging.Exceptions;

namespace EIS.Shared.RabbitMQ.Exceptions;

[Decorator]
internal sealed class MessagingErrorHandlingCommandHandlerDecorator<T> : ICommandHandler<T> where T : class, ICommand
{
    private readonly ICommandHandler<T> _handler;
    private readonly IContextProvider _contextProvider;
    private readonly IMessagingExceptionPolicyHandler _messagingExceptionPolicyHandler;

    public MessagingErrorHandlingCommandHandlerDecorator(ICommandHandler<T> handler, IContextProvider contextProvider,
        IMessagingExceptionPolicyHandler messagingExceptionPolicyHandler)
    {
        _handler = handler;
        _contextProvider = contextProvider;
        _messagingExceptionPolicyHandler = messagingExceptionPolicyHandler;
    }

    public Task HandleAsync(T command, CancellationToken cancellationToken = default)
    {
        var context = _contextProvider.Current();
        if (string.IsNullOrWhiteSpace(context.MessageId))
        {
            return _handler.HandleAsync(command, cancellationToken);
        }

        return _messagingExceptionPolicyHandler.HandleAsync(command,
            () => _handler.HandleAsync(command, cancellationToken));
    }
}