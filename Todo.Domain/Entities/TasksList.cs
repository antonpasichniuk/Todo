using System.Diagnostics.CodeAnalysis;
using Todo.Common.Result;
using Todo.Domain.Entities.Common;

namespace Todo.Domain.Entities
{
    public class TasksList : AuditableEntity, IEntity<int>
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public int CreatedById { get; set; }
        public User? CreatedBy { get; set; }

        public ICollection<TasksListAccess>? Accesses { get; set; }

        private TasksList() {}

        [SetsRequiredMembers]
        private TasksList(string name, int createdById, TasksListAccess ownerAccess)
        {
            Name = name;
            CreatedById = createdById;
            CreatedAt = DateTime.UtcNow;
            ModifiedAt = DateTime.UtcNow;
            Accesses = [ownerAccess];
        }

        public static Result<TasksList> Create(string name, int createdById) 
        {
            if (!IsNameValid(name))
            {
                return Result.Validation("Tasks list name should be within 1 and 255");
            }

            if (createdById == default)
            {
                return Result.Validation("Tasks list creator should be defined");
            }

            var ownerAccessResult = TasksListAccess.CreateOwnerAccess(createdById);

            if (ownerAccessResult.IsFailure)
            {
                return ownerAccessResult.Error;
            }

            return new TasksList(name, createdById, ownerAccessResult.Value);
        }
         
        public Result<TasksList> Update(string name)
        {
            if (!IsNameValid(name))
            {
                Result.Validation("Tasks list name should be within 1 and 255");
            }

            return this;
        }

        private static bool IsNameValid(string name)
        {
            return name.Length >= 1 || name.Length <= 255;
        }
    }
}
