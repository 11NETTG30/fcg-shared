namespace FCG.Shared.Domain.Abstractions;

public interface IAuditavel
{
    DateTime DataCriacao { get; }
    DateTime? DataAtualizacao { get; }
}
