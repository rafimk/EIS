using EIS.Shared.Abstractions;

namespace EIS.Shared.Messaging.Exceptions;

public record FailedMessage(IMessage Message);