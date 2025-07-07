using Microsoft.EntityFrameworkCore;

namespace Todo.Infrastructure.Data.Context
{
    public partial class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> dbContextOptions) : base(dbContextOptions)
        {
        }
    }
}
