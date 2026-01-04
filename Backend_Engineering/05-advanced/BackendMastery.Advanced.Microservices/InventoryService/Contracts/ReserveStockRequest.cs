namespace InventoryService.Contracts;

/// <summary>
/// Inventory reservation request.
/// 
/// WHY:
/// Inventory owns stock decisions.
/// Other services must ask — not assume.
/// </summary>
public record ReserveStockRequest(
    string Product,
    int Quantity
);