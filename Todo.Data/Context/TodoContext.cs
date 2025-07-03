using Microsoft.EntityFrameworkCore;

namespace Todo.Data.Context
{
    public partial class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> dbContextOptions) : base(dbContextOptions)
        {
        }
    }
}
