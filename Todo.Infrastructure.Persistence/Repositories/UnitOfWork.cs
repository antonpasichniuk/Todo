using Microsoft.EntityFrameworkCore;
using Todo.Application.Repositories.Interfaces;

namespace Todo.Infrastructure.Data.Repositories
{
    public class UnitOfWork<TContext>(TContext dbContext) : IUnitOfWork where TContext : DbContext
    {
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
