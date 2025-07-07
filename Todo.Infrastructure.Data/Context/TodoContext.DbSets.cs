using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;

namespace Todo.Infrastructure.Data.Context
{
    public partial class TodoContext
    {
        DbSet<User> Users { get; set; }
        DbSet<TasksList> TasksLists { get; set; }
        DbSet<TasksListAccess> TasksListAccesses { get; set; }
    }
}
