namespace OrderService.Services;

using global::OrderService.Contracts;
using System.Net.Http.Json;

/// <summary>
/// Order orchestration logic.
/// 
/// INTUITION:
/// This service coordinates, not controls.
/// 
/// FAILURE MODE:
/// Network failures are NORMAL in microservices.
/// </summary>
public class OrderService
{
    private readonly HttpClient _http;

    public OrderService(HttpClient http)
    {
        _http = http;
    }

    public async Task PlaceOrderAsync(PlaceOrderRequest request)
    {
        // Reserve inventory
        var inventoryResponse = await _http.PostAsJsonAsync(
            "http://localhost:5002/api/inventory/reserve",
            new { request.Product, request.Quantity }
        );

        if (!inventoryResponse.IsSuccessStatusCode)
            throw new InvalidOperationException("Inventory unavailable");

        // Process payment
        var paymentResponse = await _http.PostAsJsonAsync(
            "http://localhost:5001/api/payments",
            new { OrderId = Guid.NewGuid(), Amount = request.Price }
        );

        if (!paymentResponse.IsSuccessStatusCode)
            throw new InvalidOperationException("Payment failed");

        // Order would be persisted here
    }
}