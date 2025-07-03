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
    }
}
