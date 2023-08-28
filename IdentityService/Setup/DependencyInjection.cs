using IdentityService.Application.Services;
using IdentityService.Database;
using IdentityService.Database.AutoMigrations;
using IdentityService.Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Setup;

public static class DependencyInjection
{
    public static IServiceCollection RegisterDependencies(this IServiceCollection services, IConfiguration appConfiguration)
    {
        services.AddDbContext<IdentityDbContext>(opt => opt.UseNpgsql(appConfiguration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IMigrationsManager, MigrationsManager>();
        services.AddScoped<IJWTService, JWTService>();
        return services;
    }
}