using System.Linq.Expressions;
using Todo.Application.Models.Projections;
using Todo.Application.Models.Projections.Common;
using Todo.Application.Models.Requests.Page;
using Todo.Application.Models.Requests.TasksList;
using Todo.Application.Models.Requests.TasksList.Page;
using Todo.Application.Services.Interfaces;
using Todo.Common.Result;
using Todo.Domain.Entities;
using Todo.Domain.Enums;
using Todo.Domain.Interfaces;
using Todo.Domain.Interfaces.Repositories;

namespace Todo.Application.Services
{
    public class TasksListService(IUserContext userContext, ITasksListRepository tasksListRepository, IUserRepository userRepository, IUnitOfWork unitOfWork) : ITasksListService
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

            tasksListRepository.Update(tasksList);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new TasksListProjection
            {
                Id = tasksList.Id,
                Name = tasksList.Name
            };
        }

        public async Task<Result<int>> DeleteTasksListAsync(int id, CancellationToken cancellationToken = default)
        {
            var hasOwnerAccess = await tasksListRepository.HasAccess(id, userContext.Id, TasksListAccessRole.Owned, cancellationToken: cancellationToken);

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
            var hasAccess = await tasksListRepository.HasAccess(id, userContext.Id, TasksListAccessRole.Owned, cancellationToken: cancellationToken);

            if (!hasAccess)
            {
                return Result.Authorization("User has no access");
            }

            var tasksListResult = await tasksListRepository.FirstOrDefaultAsync(x => x.Id == id, true, cancellationToken: cancellationToken);

            if (tasksListResult == null)
            {
                return Result.NotFound("Task was not found");
            }

            return new TasksListProjection { Id = tasksListResult.Id, Name = tasksListResult.Name };
        }

        public async Task<Result<AggregatedList<TasksListProjection>>> GetTasksListPageAsync(TasksListFiltering filtering, PagePagination pagination, CancellationToken cancellationToken = default)
        {
            Expression<Func<TasksList, bool>> isAvailableForUser = x => x.Accesses!.Any(access => access.UserId == userContext.Id);

            var totalCount = await tasksListRepository.CountAsync(isAvailableForUser, cancellationToken: cancellationToken);

            var tasksLists = await tasksListRepository.GetOrderedAsync(
                isAvailableForUser, 
                pagination.PageSize, 
                pagination.Skip, 
                cancellationToken: cancellationToken);
            
        }

        private Expression<Func<TasksList, TOrder>> GetTasksListPageOrder<TOrder>(TasksListFiltering tasksListFiltering)
        {
            return tasksListFiltering.Sorting.By switch
            {
                TasksListPageSorting.Created => (Expression<Func<TasksList, TOrder>>)(x => x.CreatedAt),
                _ => x => x.CreatedAt
            };
        }
    }
}
