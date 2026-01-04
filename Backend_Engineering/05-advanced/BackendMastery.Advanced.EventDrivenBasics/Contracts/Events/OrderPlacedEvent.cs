namespace BackendMastery.Advanced.EventDrivenBasics.Contracts.Events;

/// <summary>
/// Represents a fact that already happened.
/// 
/// WHY:
/// Events are immutable facts, not commands.
/// Consumers react — they do not influence the producer.
/// </summary>
public record OrderPlacedEvent(
    Guid OrderId,
    string Product,
    int Quantity,
    DateTime OccurredAt
);