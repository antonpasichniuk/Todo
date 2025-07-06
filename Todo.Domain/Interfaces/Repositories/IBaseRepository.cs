using System.Linq.Expressions;

namespace Todo.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<TId, TEntity>
        where TId : struct
        where TEntity : class
    {
        Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = false, CancellationToken cancellationToken = default);
        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = false, CancellationToken cancellationToken = default);
        Task<TEntity> FirstAsync(TId id, bool asNoTracking = false, CancellationToken cancellationToken = default);
        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = false, CancellationToken cancellationToken = default);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = false, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(TId id, CancellationToken cancellationToken = default);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}
