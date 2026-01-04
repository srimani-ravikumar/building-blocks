namespace OrderService.Contracts;

/// <summary>
/// Place order request.
/// 
/// WHY:
/// Order service coordinates workflow,
/// but does not own payment or inventory logic.
/// </summary>
public record PlaceOrderRequest(
    string Product,
    int Quantity,
    decimal Price
);