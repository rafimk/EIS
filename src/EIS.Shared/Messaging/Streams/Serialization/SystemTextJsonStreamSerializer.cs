using System.Text.Json;
using System.Text.Json.Serialization;
using EIS.Shared.Abstractions;

namespace EIS.Shared.Messaging.Streams.Serialization;

public sealed class SystemTextJsonStreamSerializer : IStreamSerializer
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = {new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)}
    };

    public byte[] Serialize<T>(T message) where T : IMessage => JsonSerializer.SerializeToUtf8Bytes(message, Options);

    public T? Deserialize<T>(byte[] bytes) where T : IMessage => JsonSerializer.Deserialize<T>(bytes, Options);
}