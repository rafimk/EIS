using EIS.Shared.Messaging.Brokers;

namespace EIS.Shared.Messaging.Exceptions;

public record MessageExceptionPolicy(bool Retry, bool UseDeadLetter, Func<IMessageBroker, Task>? Publish = null);