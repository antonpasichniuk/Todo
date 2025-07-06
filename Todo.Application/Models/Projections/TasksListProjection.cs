using Todo.Application.Models.Projections.Common;

namespace Todo.Application.Models.Projections
{
    public class TasksListProjection
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
