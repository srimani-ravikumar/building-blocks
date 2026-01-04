namespace BackendMastery.Advanced.CQRS.Controllers;

using BackendMastery.Advanced.CQRS.Services.Queries;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Query API surface.
/// </summary>
[ApiController]
[Route("api/orders")]
public class OrdersQueryController : ControllerBase
{
    private readonly OrderQueryService _service;

    public OrdersQueryController(OrderQueryService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetOrders()
        => Ok(_service.GetAllOrders());
}