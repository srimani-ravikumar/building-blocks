namespace BackendMastery.Advanced.CQRS.Services.Queries;

using BackendMastery.Advanced.CQRS.Contracts.Queries;
using BackendMastery.Advanced.CQRS.Infrastructure;

/// <summary>
/// Handles read-only data access.
/// 
/// INTUITION:
/// Queries optimize for shape and speed.
/// They must never change state.
///
/// FAILURE MODE:
/// If queries modify data, retries and caching break.
/// </summary>
public class OrderQueryService
{
    private readonly InMemoryOrderStore _store;

    public OrderQueryService(InMemoryOrderStore store)
    {
        _store = store;
    }

    public IEnumerable<OrderSummaryResponse> GetAllOrders()
    {
        return _store.GetAll()
            .Select(o => new OrderSummaryResponse
            {
                OrderId = o.Id,
                Product = o.Product,
                Quantity = o.Quantity
            });
    }
}