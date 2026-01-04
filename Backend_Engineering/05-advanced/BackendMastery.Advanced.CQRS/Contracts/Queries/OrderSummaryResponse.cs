namespace BackendMastery.Advanced.CQRS.Contracts.Queries;

/// <summary>
/// Read projection.
/// 
/// WHY:
/// Read models evolve independently from write models.
/// </summary>
public class OrderSummaryResponse
{
    public Guid OrderId { get; set; }
    public string Product { get; set; } = string.Empty;
    public int Quantity { get; set; }
}