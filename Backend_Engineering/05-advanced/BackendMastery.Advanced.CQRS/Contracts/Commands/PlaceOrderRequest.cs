namespace BackendMastery.Advanced.CQRS.Contracts.Commands;

/// <summary>
/// Command DTO.
/// 
/// WHY:
/// Commands model intent, not data shape.
/// </summary>
public class PlaceOrderRequest
{
    public string Product { get; set; } = string.Empty;
    public int Quantity { get; set; }
}