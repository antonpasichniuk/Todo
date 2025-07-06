using System.Linq.Expressions;
using Todo.Domain.Entities;
using Todo.Domain.Enums;

namespace Todo.Domain.Interfaces.Repositories
{
    public interface ITasksListRepository : IBaseRepository<int, TasksList>
    {
        Task<bool> HasAccess(int tasksList, int userId, TasksListAccessRole? role = null, CancellationToken cancellationToken = default);
        Task<List<TasksList>> GetSortedAsync<TOrder>(
            Expression<Func<TasksList, bool>> predicate,
            int count,
            int skip,
            Expression<Func<TasksList, TOrder>> orderExpression, 
            bool asc = true,
            CancellationToken cancellationToken = default);
    }
}
