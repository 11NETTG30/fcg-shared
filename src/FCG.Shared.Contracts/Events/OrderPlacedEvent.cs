namespace FCG.Shared.Contracts.Events;

public sealed record OrderPlacedEvent(Guid GameId, Guid UserId, decimal Price, string Email, string? TituloJogo = null);
