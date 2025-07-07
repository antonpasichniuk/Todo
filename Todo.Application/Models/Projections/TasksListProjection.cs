using System.Linq.Expressions;
using Todo.Application.Models.Projections.Common;
using Todo.Domain.Entities;

namespace Todo.Application.Models.Projections
{
    public class TasksListProjection
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public static Expression<Func<TasksList, TasksListProjection>> FromDomain => tasksList => new TasksListProjection
        {
            Id = tasksList.Id,
            Name = tasksList.Name,
            CreatedAt = tasksList.CreatedAt,
            ModifiedAt = tasksList.ModifiedAt
        };
    }
}
