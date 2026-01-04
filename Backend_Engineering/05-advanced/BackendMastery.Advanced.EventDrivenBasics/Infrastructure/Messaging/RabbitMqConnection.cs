namespace BackendMastery.Advanced.EventDrivenBasics.Infrastructure.Messaging;

using RabbitMQ.Client;

/// <summary>
/// Manages RabbitMQ connection lifecycle.
/// 
/// WHY:
/// RabbitMQ.Client v7 uses async-first APIs.
/// Connections are long-lived; channels are short-lived.
/// </summary>
public class RabbitMqConnection
{
    public async Task<IConnection> CreateAsync()
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost"
        };

        return await factory.CreateConnectionAsync();
    }
}