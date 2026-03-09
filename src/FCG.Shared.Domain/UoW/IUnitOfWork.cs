namespace FCG.Shared.Domain.UoW;

public interface IUnitOfWork
{
    Task<bool> Commit();
}
