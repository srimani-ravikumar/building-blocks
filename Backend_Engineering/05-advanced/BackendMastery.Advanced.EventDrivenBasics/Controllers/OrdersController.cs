namespace BackendMastery.Advanced.EventDrivenBasics.Controllers;

using BackendMastery.Advanced.EventDrivenBasics.Services;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// API surface remains synchronous.
/// 
/// RULE:
/// HTTP ends at intent.
/// Events handle consequences.
/// </summary>
[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly OrderService _service;

    public OrdersController(OrderService service)
    {
        _service = service;
    }

    [HttpPost]
    public IActionResult PlaceOrder(string product, int quantity)
    {
        _service.PlaceOrder(product, quantity);
        return Accepted();
    }
}