namespace Todo.Application.Models.Projections.Common
{
    public class AggregatedList<TEntity>
    {
        public TEntity[] Items { get; set; }
        public int TotalCount { get; set; }
    }
}
