namespace Todo.Application.Models.Requests.TasksList
{
    public class UpdateTasksList
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
