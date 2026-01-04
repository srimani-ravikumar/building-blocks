using InventoryService.Contracts;
using Microsoft.AspNetCore.Mvc;


namespace InventoryService.Controllers;

[ApiController]
[Route("api/inventory")]
public class InventoryController : ControllerBase
{
    private readonly Services.InventoryService _service;

    public InventoryController(Services.InventoryService service)
    {
        _service = service;
    }

    [HttpPost("reserve")]
    public IActionResult Reserve(ReserveStockRequest request)
    {
        if (!_service.Reserve(request.Product, request.Quantity))
            return Conflict("Insufficient stock");

        return Ok();
    }
}