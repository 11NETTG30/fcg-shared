namespace FCG.Shared.Contracts.Events;

public sealed record UserCreatedEvent(Guid UserId, string Email, string Nome);
