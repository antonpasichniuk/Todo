using Microsoft.Extensions.DependencyInjection;
using Todo.Application.Services;
using Todo.Application.Services.Interfaces;

namespace Todo.Application
{
    public static class DependencyInjection
    {
        public static void AddServices(IServiceCollection services)
        {
            services.AddScoped<ITasksListService, TasksListService>();
        }
    }
}
