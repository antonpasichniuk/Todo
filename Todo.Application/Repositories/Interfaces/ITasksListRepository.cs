using Todo.Application.Models.Requests.Page;
using Todo.Application.Models.Requests.TasksList.Page;
using Todo.Domain.Entities;

namespace Todo.Application.Repositories.Interfaces
{
    public interface ITasksListRepository : IBaseRepository<int, TasksList>
    {
        Task<bool> HasAccess(int tasksListId, int userId, CancellationToken cancellationToken = default);
        Task<bool> HasOwnerAccess(int tasksListId, int userId, CancellationToken cancellationToken = default);
        Task<List<TasksList>> GetPageAsync(int userId, TasksListFiltering filtering, PagePagination pagination, CancellationToken cancellationToken = default);
        Task<int> CountPageAsync(int userId, TasksListFiltering filtering, CancellationToken cancellationToken = default);
    }
}
