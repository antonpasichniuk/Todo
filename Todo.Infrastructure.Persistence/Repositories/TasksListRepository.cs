using Microsoft.EntityFrameworkCore;
using System.Data;
using Todo.Application.Models.Requests.Page;
using Todo.Application.Models.Requests.TasksList.Page;
using Todo.Infrastructure.Data.Context;
using Todo.Domain.Entities;
using Todo.Domain.Enums;
using Todo.Application.Repositories.Interfaces;

namespace Todo.Infrastructure.Data.Repositories
{
    public class TasksListRepository : BaseRepository<int, TasksList>, ITasksListRepository
    {
        public TasksListRepository(TodoContext dbContext) : base(dbContext) {}

        public Task<bool> HasAccess(int tasksListId, int userId, CancellationToken cancellationToken = default)
        {
            return _dbSet.AnyAsync(x => x.Id == tasksListId && x.Accesses.Any(access => access.UserId == userId), cancellationToken);
        }

        public Task<bool> HasOwnerAccess(int tasksListId, int userId, CancellationToken cancellationToken = default)
        {
            return _dbSet.AnyAsync(x => x.Id == tasksListId 
                && x.Accesses.Any(access => access.UserId == tasksListId && access.AccessRole == TasksListAccessRole.Owned), cancellationToken);
        }

        public Task<List<TasksList>> GetPageAsync(
            int userId, 
            TasksListFiltering filtering, 
            PagePagination pagination, 
            CancellationToken cancellationToken = default)
        {
            var filteredQuery = GetFilteredQuery(_dbSet, userId, filtering);
            var sortedQuery = GetSortedQuery(filteredQuery, filtering.Sorting);

            return sortedQuery.Skip(pagination.Skip).Take(pagination.PageSize).ToListAsync(cancellationToken: cancellationToken);
        }

        public Task<int> CountPageAsync(int userId, TasksListFiltering filtering, CancellationToken cancellationToken = default)
        {
            return GetFilteredQuery(_dbSet, userId, filtering).CountAsync(cancellationToken);
        }

        private static IQueryable<TasksList> GetFilteredQuery(IQueryable<TasksList> query, int userId, TasksListFiltering filtering)
        {
            var filteredQuery = GetSecureQuery(query, userId, true);

            // For now method is useless, but if there was a filtration, it will be easier to implement it here
            return filteredQuery;
        }

        private static IQueryable<TasksList> GetSortedQuery(IQueryable<TasksList> query, PageSorting<TasksListSorting>? sorting = null)
        {
            if (sorting?.By == TasksListSorting.Created)
            {
                return sorting.Asc ? query.OrderBy(x => x.CreatedAt) : query.OrderByDescending(x => x.CreatedAt);
            }

            return query.OrderBy(x => x.CreatedAt);
        }

        private static IQueryable<TasksList> GetSecureQuery(IQueryable<TasksList> query, int userId, bool asNoTracking = false)
        {
            return ApplyAsNoTracking(query, asNoTracking)
                .Where(x => x.Accesses.Any(access => access.UserId == userId));
        }
    }
}
