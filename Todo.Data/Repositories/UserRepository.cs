using Microsoft.EntityFrameworkCore;
using Todo.Data.Context;
using Todo.Domain.Entities;
using Todo.Domain.Interfaces.Repositories;

namespace Todo.Data.Repositories
{
    public class UserRepository(TodoContext dbContext) : BaseRepository<int, User>(dbContext), IUserRepository
    {
    }
}
