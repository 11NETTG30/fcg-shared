using FCG.Shared.Domain.UoW;
using Microsoft.EntityFrameworkCore;

namespace FCG.Shared.Infrastructure.Persistence.UoW;

public abstract class DbContextUoW: DbContext, IUnitOfWork
{
    protected DbContextUoW(DbContextOptions options) : base(options)
    {

    }

    public async Task<bool> Commit()
    {
        return await SaveChangesAsync() > 0;
    }
}
