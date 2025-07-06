namespace Todo.Domain.Entities.Common
{
    public interface IHasId<TId>
    {
        TId Id { get; set; }
    }
}
