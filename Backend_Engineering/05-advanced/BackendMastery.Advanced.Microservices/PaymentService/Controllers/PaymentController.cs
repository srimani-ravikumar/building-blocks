using Microsoft.AspNetCore.Mvc;
using PaymentService.Contracts;

namespace PaymentService.Controllers;
[ApiController]
[Route("api/payments")]
public class PaymentsController : ControllerBase
{
    private readonly Services.PaymentService _service;

    public PaymentsController(Services.PaymentService service)
    {
        _service = service;
    }

    [HttpPost]
    public IActionResult Pay(ProcessPaymentRequest request)
    {
        if (!_service.Process(request.Amount))
            return StatusCode(402, "Payment failed");

        return Ok();
    }
}
