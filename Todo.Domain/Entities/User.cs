using Todo.Domain.Entities.Common;

namespace Todo.Domain.Entities
{
    public class User : IEntity<int>
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
