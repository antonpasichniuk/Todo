namespace Todo.Web.Models.Responses.Common
{
    public class PagedList<TItem>
    {
        public required TItem[] Items { get; set; }
        public required PagedMetadata Metadata { get; set; }
    }
}
