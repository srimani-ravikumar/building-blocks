namespace BackendMastery.Advanced.EventDrivenBasics.Infrastructure.Messaging;

using System.Text;
using System.Text.Json;
using BackendMastery.Advanced.EventDrivenBasics.Contracts.Events;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

/// <summary>
/// Asynchronous event consumer.
/// 
/// WHY:
/// v7 enforces async handlers to avoid thread starvation.
/// </summary>
public class EventConsumer
{
    public async Task StartAsync()
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost"
        };

        var connection = await factory.CreateConnectionAsync();
        var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: "order-events",
            durable: true,
            exclusive: false,
            autoDelete: false
        );

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += async (_, args) =>
        {
            var json = Encoding.UTF8.GetString(args.Body.ToArray());
            var evt = JsonSerializer.Deserialize<OrderPlacedEvent>(json);

            Console.WriteLine(
                $"[EVENT] Order {evt!.OrderId} processed"
            );

            // Simulate async work
            await Task.Delay(50);
        };

        await channel.BasicConsumeAsync(
            queue: "order-events",
            autoAck: true,
            consumer: consumer
        );
    }
}