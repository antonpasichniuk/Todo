namespace Todo.Domain.Entities
{
    public class TasksList
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public int CreatedById { get; set; }
        public User? CreatedBy { get; set; }

        public ICollection<TasksListAccess>? Accesses { get; set; }
    }
}
