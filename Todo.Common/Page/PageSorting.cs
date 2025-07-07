namespace Todo.Application.Models.Requests.Page
{
    public class PageSorting<TSoring> where TSoring : struct
    {
        public TSoring By { get; set; }
        public bool Asc { get; set; } = true;
    }
}
