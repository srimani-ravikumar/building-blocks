namespace PaymentService.Services;

/// <summary>
/// Payment processing logic.
/// 
/// FAILURE MODE:
/// Payment retries must be isolated.
/// </summary>
public class PaymentService
{
    public bool Process(decimal amount)
    {
        return amount <= 5000;
    }
}