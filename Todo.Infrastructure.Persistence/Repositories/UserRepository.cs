using Todo.Infrastructure.Data.Context;
using Todo.Domain.Entities;
using Todo.Application.Repositories.Interfaces;

namespace Todo.Infrastructure.Data.Repositories
{
    public class UserRepository(TodoContext dbContext) : BaseRepository<int, User>(dbContext), IUserRepository
    {
    }
}
