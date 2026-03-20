namespace FCG.Shared.Contracts.Events;

public sealed record PaymentProcessedEvent(
    Guid OrderId,
    Guid PaymentId,
    Guid UserId,
    Guid GameId,
    decimal Price,
    string Email,
    string Status
);
