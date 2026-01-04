namespace InventoryService.Services;

/// <summary>
/// Inventory domain logic.
/// 
/// FAILURE MODE:
/// If inventory logic leaks into OrderService,
/// stock consistency will break.
/// </summary>
public class InventoryService
{
    public bool Reserve(string product, int quantity)
    {
        // Simulate stock check
        return quantity <= 10;
    }
}