namespace Todo.Application.Models.Requests.Page
{
    public class PagePagination
    {
        public int? PageSize { get; set; } = 20;
        public int? CurrentPage { get; set; } = 1;

        public int Skip => PageSize!.Value * (CurrentPage!.Value - 1);
    }
}
