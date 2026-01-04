namespace BackendMastery.Advanced.CQRS.Infrastructure;

/// <summary>
/// Represents a shared persistence store.
/// 
/// WHY:
/// CQRS does NOT require separate databases.
/// This store is intentionally shared to prove the point.
///
/// WHAT BREAKS IF MISUSED:
/// If command logic leaks into query services,
/// separation collapses and CQRS loses value.
/// </summary>
public class InMemoryOrderStore
{
    private readonly Dictionary<Guid, OrderRecord> _orders = new();

    public void Save(OrderRecord order)
        => _orders[order.Id] = order;

    public OrderRecord? Get(Guid id)
        => _orders.TryGetValue(id, out var order) ? order : null;

    public IEnumerable<OrderRecord> GetAll()
        => _orders.Values;
}

public record OrderRecord(
    Guid Id,
    string Product,
    int Quantity,
    DateTime CreatedAt
);