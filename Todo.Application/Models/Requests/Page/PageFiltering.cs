namespace Todo.Application.Models.Requests.Page
{
    public class PageFiltering<TSorting> where TSorting : struct
    {
        public PageSorting<TSorting>? Sorting { get; set; }
    }
}
