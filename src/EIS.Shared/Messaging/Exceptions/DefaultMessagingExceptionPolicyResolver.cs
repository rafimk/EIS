using EIS.Shared.Abstractions;

namespace EIS.Shared.Messaging.Exceptions;

internal sealed class DefaultMessagingExceptionPolicyResolver : IMessagingExceptionPolicyResolver
{
    public MessageExceptionPolicy? Resolve(IMessage message, Exception exception) => null;
}