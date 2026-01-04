using Microsoft.AspNetCore.Mvc;
using OrderService.Contracts;

namespace OrderService.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly Services.OrderService _service;

    public OrdersController(Services.OrderService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> PlaceOrder(PlaceOrderRequest request)
    {
        await _service.PlaceOrderAsync(request);
        return Accepted();
    }
}