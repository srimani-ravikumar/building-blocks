namespace BackendMastery.Advanced.EventDrivenBasics.Infrastructure.Messaging;

using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

/// <summary>
/// Publishes domain events.
/// 
/// INTUITION:
/// Publishing must be fast and non-blocking.
/// Channels are created per operation.
/// </summary>
public class EventPublisher
{
    private readonly RabbitMqConnection _connection;

    public EventPublisher(RabbitMqConnection connection)
    {
        _connection = connection;
    }

    public async Task PublishAsync<T>(T @event)
    {
        var connection = await _connection.CreateAsync();
        await using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: "order-events",
            durable: true,
            exclusive: false,
            autoDelete: false
        );

        var body = Encoding.UTF8.GetBytes(
            JsonSerializer.Serialize(@event)
        );

        await channel.BasicPublishAsync(
            exchange: "",
            routingKey: "order-events",
            body: body
        );
    }
}