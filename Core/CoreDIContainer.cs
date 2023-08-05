using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class CoreDIContainer
{
    public static void ConfigureCoreDIContainer(this IServiceCollection services,
        IWebHostEnvironment hostingEnvironment,
        IConfiguration configuration)
    {
        // services.AddSingleton<IMigrationsManager, MigrationsManager>();
    }
}