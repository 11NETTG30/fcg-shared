namespace FCG.Shared.Contracts.Events;

public sealed record UserCreatedEvent(Guid UsuarioId, string Email, string Nome);
