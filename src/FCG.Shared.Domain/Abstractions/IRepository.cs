using FCG.Shared.Domain.UoW;

namespace FCG.Shared.Domain.Abstractions;

public interface IRepository<T> : IDisposable where T : Entity, IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }
}
