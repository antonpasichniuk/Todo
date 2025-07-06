using Microsoft.EntityFrameworkCore;
using Todo.Domain.Interfaces;

namespace Todo.Data.Repositories
{
    public class UnitOfWork<TContext>(TContext dbContext) : IUnitOfWork where TContext : DbContext
    {
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
