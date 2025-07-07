using Todo.Domain.Entities;

namespace Todo.Application.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<int, User>
    {
    }
}
