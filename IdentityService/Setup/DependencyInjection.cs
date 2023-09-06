using System.Reflection;
using Core.Database;
using FluentValidation;
using IdentityService.Application.Services;
using IdentityService.Database;
using IdentityService.Database.AutoMigrations;
using IdentityService.Database.Repository;
using IdentityService.Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Setup;

public static class DependencyInjection
{
    private static Assembly ApplicationAssembly => Assembly.GetExecutingAssembly();
    public static IServiceCollection RegisterDependencies(this IServiceCollection services, IConfiguration appConfiguration)
    {
        services.AddDbContext<IdentityDbContext>(opt => opt.UseNpgsql(appConfiguration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IMigrationsManager, MigrationsManager>();
        services.AddValidatorsFromAssembly(ApplicationAssembly);
        services.AddScoped<UserManager>();
        services.AddScoped<IRepository, Repository>();
        services.AddScoped<IJWTService, JWTService>();
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }
}