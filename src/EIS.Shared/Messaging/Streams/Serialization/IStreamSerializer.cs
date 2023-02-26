using EIS.Shared.Abstractions;

namespace EIS.Shared.Messaging.Streams.Serialization;

public interface IStreamSerializer
{
    byte[] Serialize<T>(T message) where T : IMessage;
    T? Deserialize<T>(byte[] bytes) where T : IMessage;
}