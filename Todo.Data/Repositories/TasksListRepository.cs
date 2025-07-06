using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Todo.Data.Context;
using Todo.Domain.Entities;
using Todo.Domain.Enums;
using Todo.Domain.Interfaces.Repositories;

namespace Todo.Data.Repositories
{
    public class TasksListRepository : BaseRepository<int, TasksList>, ITasksListRepository
    {
        public TasksListRepository(TodoContext dbContext) : base(dbContext) {}

        public Task<bool> HasAccess(int tasksListId, int userId, TasksListAccessRole? role, CancellationToken cancellationToken = default)
        {
            return _dbSet.AnyAsync(x => x.Accesses.Any(access => access.UserId == tasksListId && (!role.HasValue || role.Value == access.AccessRole)), cancellationToken);
        }

        public Task<List<TasksList>> GetSortedAsync<TOrder>(
            Expression<Func<TasksList, bool>> predicate,
            int count,
            int skip,
            Expression<Func<TasksList, TOrder>> orderExpression,
            bool asc = true,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyAsNoTracking(_dbSet, true)
                .Where(predicate);
            
            var orderedQuery = asc ? query.OrderBy(orderExpression) : query.OrderByDescending(orderExpression);

            return orderedQuery.Skip(skip).Take(count).ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
