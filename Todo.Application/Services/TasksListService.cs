using Todo.Application.Models.Projections;
using Todo.Application.Models.Projections.Common;
using Todo.Application.Models.Requests.Page;
using Todo.Application.Models.Requests.TasksList;
using Todo.Application.Models.Requests.TasksList.Page;
using Todo.Application.Repositories.Interfaces;
using Todo.Application.Services.Interfaces;
using Todo.Common.Result;
using Todo.Domain.Entities;

namespace Todo.Application.Services
{
    public class TasksListService(IUserContext userContext, ITasksListRepository tasksListRepository, IUnitOfWork unitOfWork) : ITasksListService
    {
        public async Task<Result<TasksListProjection>> CreateTasksListAsync(CreateTasksList request, CancellationToken cancellationToken = default)
        {
            var newTasksListResult = TasksList.Create(request.Name, userContext.Id);

            if (newTasksListResult.IsFailure)
            {
                return newTasksListResult.Error;
            }

            tasksListRepository.Add(newTasksListResult.Value!);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            var createdTasksList = await tasksListRepository.FirstAsync(newTasksListResult.Value!.Id, false, cancellationToken);

            return new TasksListProjection
            {
                Id = createdTasksList.Id,
                Name = createdTasksList.Name
            };
        }

        public async Task<Result<TasksListProjection>> UpdateTasksListAsync(UpdateTasksList request, CancellationToken cancellationToken = default)
        {
            var hasUserAccess = await tasksListRepository.HasAccess(request.Id, userContext.Id, cancellationToken: cancellationToken);

            if (!hasUserAccess)
            {
                return Result.Authorization("User has no access");
            }

            var tasksList = await tasksListRepository.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

            if (tasksList == null)
            {
                return Result.NotFound("Tasks list was not found");
            }

            var updatedTasksList = tasksList.Update(request.Name);

            if (updatedTasksList.IsFailure)
            {
                return updatedTasksList.Error;
            }

            tasksListRepository.Update(tasksList);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return TasksListProjection.FromDomain.Compile().Invoke(tasksList);
        }

        public async Task<Result<int>> DeleteTasksListAsync(int id, CancellationToken cancellationToken = default)
        {
            var hasOwnerAccess = await tasksListRepository.HasOwnerAccess(id, userContext.Id, cancellationToken: cancellationToken);

            if (!hasOwnerAccess)
            {
                return Result.Authorization("User has no access");
            }

            var tasksList = await tasksListRepository.FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);

            if (tasksList == null)
            {
                return Result.NotFound("Tasks list was not found");
            }

            tasksListRepository.Remove(tasksList);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return tasksList.Id;
        }

        public async Task<Result<TasksListProjection>> GetTasksListByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var hasAccess = await tasksListRepository.HasOwnerAccess(id, userContext.Id, cancellationToken: cancellationToken);

            if (!hasAccess)
            {
                return Result.Authorization("User has no access");
            }

            var tasksListResult = await tasksListRepository.FirstOrDefaultAsync(x => x.Id == id, true, cancellationToken: cancellationToken);

            if (tasksListResult == null)
            {
                return Result.NotFound("Task was not found");
            }

            return TasksListProjection.FromDomain.Compile().Invoke(tasksListResult);
        }

        public async Task<Result<AggregatedList<TasksListProjection>>> GetTasksListPageAsync(TasksListFiltering filtering, PagePagination pagination, CancellationToken cancellationToken = default)
        {
            var totalCount = await tasksListRepository.CountPageAsync(userContext.Id, filtering, cancellationToken: cancellationToken);
            var tasksLists = await tasksListRepository.GetPageAsync(userContext.Id, filtering, pagination, cancellationToken: cancellationToken);

            return new AggregatedList<TasksListProjection>
            {
                Items = [.. tasksLists.Select(TasksListProjection.FromDomain.Compile())],
                TotalCount = totalCount
            };
        }
    }
}
