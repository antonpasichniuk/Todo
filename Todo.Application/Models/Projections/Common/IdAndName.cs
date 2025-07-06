namespace Todo.Application.Models.Projections.Common
{
    public class IdAndName<TId>
    {
        public required TId Id { get; set; }
        public string? Name { get; set; }
    }
}
