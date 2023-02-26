namespace EIS.Shared.RabbitMQ;

public sealed class RabbitMQOptions
{
    public bool Enabled { get; set; }
    public string ConnectionString { get; set; } = string.Empty;
}