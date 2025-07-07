using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Todo.Infrastructure.Data.Context;
using Todo.Domain.Entities.Common;
using Todo.Application.Repositories.Interfaces;

namespace Todo.Infrastructure.Data.Repositories
{
    public class BaseRepository<TId, TEntity>(TodoContext dbContext) : IBaseRepository<TId, TEntity>
        where TId : struct
        where TEntity : class, IEntity<TId>
    {
        protected readonly DbSet<TEntity> _dbSet = dbContext.Set<TEntity>();

        public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return ApplyAsNoTracking(_dbSet, asNoTracking).CountAsync(predicate, cancellationToken);
        }

        public Task<bool> ExistsAsync(TId id, CancellationToken cancellationToken = default)
        {
            return _dbSet.AnyAsync(x => x.Id.Equals(id), cancellationToken);
        }

        public Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return ApplyAsNoTracking(_dbSet, asNoTracking).FirstAsync(predicate, cancellationToken);
        }

        public Task<TEntity> FirstAsync(TId id, bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return ApplyAsNoTracking(_dbSet, asNoTracking).FirstAsync(x => x.Id.Equals(id), cancellationToken);
        }

        public Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return ApplyAsNoTracking(_dbSet, asNoTracking).FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            return ApplyAsNoTracking(_dbSet, asNoTracking).Where(predicate).ToListAsync(cancellationToken);
        }

        public void Add(TEntity entity)
        {
            dbContext.Add(entity);
        }

        public void Remove(TEntity entity)
        {
            dbContext.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            dbContext.Update(entity);
        }

        protected static IQueryable<TEntity> ApplyAsNoTracking(IQueryable<TEntity> query, bool asNoTracking = false)
        {
            return asNoTracking ? query.AsNoTracking() : query;
        }
    }
}
