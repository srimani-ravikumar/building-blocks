namespace BackendMastery.Advanced.EventDrivenBasics.Services;

using BackendMastery.Advanced.EventDrivenBasics.Contracts.Events;
using BackendMastery.Advanced.EventDrivenBasics.Infrastructure.Messaging;

/// <summary>
/// Handles synchronous business logic.
/// 
/// INTUITION:
/// Commands must finish fast.
/// Anything non-critical is emitted as an event.
/// </summary>
public class OrderService
{
    private readonly EventPublisher _publisher;

    public OrderService(EventPublisher publisher)
    {
        _publisher = publisher;
    }

    public void PlaceOrder(string product, int quantity)
    {
        // Business validation stays synchronous
        if (quantity <= 0)
            throw new InvalidOperationException("Invalid quantity.");

        var orderId = Guid.NewGuid();

        // Publish event instead of calling other systems
        var evt = new OrderPlacedEvent(
            orderId,
            product,
            quantity,
            DateTime.UtcNow
        );

        _publisher.PublishAsync(evt).GetAwaiter().GetResult();
    }
}