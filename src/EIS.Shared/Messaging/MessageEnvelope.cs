using EIS.Shared.Abstractions;
using EIS.Shared.Contexts;

namespace EIS.Shared.Messaging;

public record MessageEnvelope<T>(T Message, MessageContext Context) where T : IMessage;