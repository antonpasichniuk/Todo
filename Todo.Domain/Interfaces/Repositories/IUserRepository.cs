using Todo.Domain.Entities;

namespace Todo.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<int, User>
    {
    }
}
