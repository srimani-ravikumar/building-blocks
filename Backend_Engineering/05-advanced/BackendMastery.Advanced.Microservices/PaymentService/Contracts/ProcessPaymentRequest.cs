namespace PaymentService.Contracts;

/// <summary>
/// Payment request.
/// 
/// WHY:
/// Payments are a separate risk domain.
/// </summary>
public record ProcessPaymentRequest(
    Guid OrderId,
    decimal Amount
);