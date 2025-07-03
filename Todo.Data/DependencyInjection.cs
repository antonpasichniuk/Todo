using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Todo.Data.Context;

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
    }
}
