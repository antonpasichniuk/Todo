using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Todo.Data.Context;
using Todo.Data.Repositories;
using Todo.Domain.Interfaces;
using Todo.Domain.Interfaces.Repositories;

namespace Todo.Data
{
    public static class DependencyInjection
    {
        public static void AddDbContext(IServiceCollection services, IConfigurationManager configuration)
        {
            var temp = configuration.GetConnectionString(nameof(TodoContext));

            services
                .AddDbContext<TodoContext>(options => 
                    options.UseNpgsql(configuration.GetConnectionString(nameof(TodoContext)))
                        .UseSnakeCaseNamingConvention()
                );
        }

        public static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<ITasksListRepository, TasksListRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork<TodoContext>>();
        }
    }
}
