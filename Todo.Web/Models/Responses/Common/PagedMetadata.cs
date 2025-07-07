namespace Todo.Web.Models.Responses.Common
{
    public class PagedMetadata
    {
        public int TotalItems { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages => TotalItems != 0 ? (int)Math.Ceiling((double)TotalItems / PageSize) : 0;
    }
}
