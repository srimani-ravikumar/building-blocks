namespace BackendMastery.Advanced.CQRS.Controllers;

using BackendMastery.Advanced.CQRS.Contracts.Commands;
using BackendMastery.Advanced.CQRS.Services.Commands;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Command API surface.
/// 
/// RULE:
/// One controller = one responsibility.
/// </summary>
[ApiController]
[Route("api/orders")]
public class OrdersCommandController : ControllerBase
{
    private readonly OrderCommandService _service;

    public OrdersCommandController(OrderCommandService service)
    {
        _service = service;
    }

    [HttpPost]
    public IActionResult PlaceOrder(PlaceOrderRequest request)
    {
        var orderId = _service.PlaceOrder(request);
        return Accepted(new { OrderId = orderId });
    }
}