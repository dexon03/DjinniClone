using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class CoreDIContainer
{
    public static IServiceCollection ConfigureCoreDIContainer(this IServiceCollection services)
    {
        // services.AddSingleton<IMigrationsManager, MigrationsManager>();
        return services;
    }
}