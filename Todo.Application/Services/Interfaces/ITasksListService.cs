using Todo.Application.Models.Projections;
using Todo.Application.Models.Requests.TasksList;
using Todo.Common.Result;

namespace Todo.Application.Services.Interfaces
{
    public interface ITasksListService
    {
        Task<Result<TasksListProjection>> CreateTasksListAsync(CreateTasksList request, CancellationToken cancellationToken = default);
        Task<Result<TasksListProjection>> UpdateTasksListAsync(UpdateTasksList request, CancellationToken cancellationToken = default);
        Task<Result<int>> DeleteTasksListAsync(int id, CancellationToken cancellationToken = default);
        Task<Result<TasksListProjection>> GetTasksListByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Result<TasksListProjection[]>> GetTasksListPageAsync(int pageSize, CancellationToken cancellationToken = default);
    }
}
