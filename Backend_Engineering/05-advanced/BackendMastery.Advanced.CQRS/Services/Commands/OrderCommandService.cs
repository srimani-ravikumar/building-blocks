namespace BackendMastery.Advanced.CQRS.Services.Commands;

using BackendMastery.Advanced.CQRS.Contracts.Commands;
using BackendMastery.Advanced.CQRS.Infrastructure;

/// <summary>
/// Handles state-changing operations.
/// 
/// INTUITION:
/// Commands enforce business rules.
/// They are about correctness, not convenience.
///
/// WHEN TO USE:
/// Any operation that mutates system state.
///
/// FAILURE MODE:
/// Returning read models from commands
/// creates tight coupling and accidental dependencies.
/// </summary>
public class OrderCommandService
{
    private readonly InMemoryOrderStore _store;

    public OrderCommandService(InMemoryOrderStore store)
    {
        _store = store;
    }

    public Guid PlaceOrder(PlaceOrderRequest request)
    {
        // Validation belongs here, not in controllers
        if (request.Quantity <= 0)
            throw new InvalidOperationException("Quantity must be positive.");

        var order = new OrderRecord(
            Guid.NewGuid(),
            request.Product,
            request.Quantity,
            DateTime.UtcNow
        );

        _store.Save(order);

        // Commands return identifiers, not projections
        return order.Id;
    }
}