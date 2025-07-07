using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Todo.Application.Repositories.Interfaces;
using Todo.Infrastructure.Data.Context;
using Todo.Infrastructure.Data.Repositories;

namespace Todo.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<ITasksListRepository, TasksListRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork<TodoContext>>();
        }
    }
}
