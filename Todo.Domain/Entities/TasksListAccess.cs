using Todo.Common.Result;
using Todo.Domain.Enums;

namespace Todo.Domain.Entities
{
    public class TasksListAccess
    {
        public int TasksListId { get; set; }
        public TasksList? TasksList { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        public TasksListAccessRole AccessRole { get; set; }

        private TasksListAccess() {}

        private TasksListAccess(int userId, TasksListAccessRole role)
        {
            UserId = userId;
            AccessRole = role;
        }

        public static Result<TasksListAccess> CreateOwnerAccess(int userId)
        {
            if (userId == default)
            {
                Result.Validation("User id is undefined");
            }

            return new TasksListAccess(userId, TasksListAccessRole.Owned);
        }
    }
}
