using EIS.Shared.Abstractions;

namespace EIS.Shared.Messaging.Exceptions;

public interface IMessagingExceptionPolicyResolver
{
    MessageExceptionPolicy? Resolve(IMessage message, Exception exception);
}