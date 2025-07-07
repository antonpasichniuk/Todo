using Todo.Application.Models.Projections;
using Todo.Application.Models.Projections.Common;
using Todo.Application.Models.Requests.Page;
using Todo.Application.Models.Requests.TasksList;
using Todo.Application.Models.Requests.TasksList.Page;
using Todo.Common.Result;

namespace Todo.Application.Services.Interfaces
{
    public interface ITasksListService
    {
        Task<Result<TasksListProjection>> CreateTasksListAsync(CreateTasksList request, CancellationToken cancellationToken = default);
        Task<Result<TasksListProjection>> UpdateTasksListAsync(UpdateTasksList request, CancellationToken cancellationToken = default);
        Task<Result<int>> DeleteTasksListAsync(int id, CancellationToken cancellationToken = default);
        Task<Result<TasksListProjection>> GetTasksListByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Result<AggregatedList<TasksListProjection>>> GetTasksListPageAsync(TasksListFiltering filtering, PagePagination pagination, CancellationToken cancellationToken = default);
    }
}
