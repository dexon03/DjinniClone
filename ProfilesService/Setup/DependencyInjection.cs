using System.Reflection;
using Core.Database;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProfilesService.Application.Services;
using ProfilesService.Database;
using ProfilesService.Database.AutoMigrations;
using ProfilesService.Database.Repository;
using ProfilesService.Domain.Contracts;

namespace ProfilesService.Setup;

public static class DependencyInjection
{
    private static Assembly ApplicationAssembly => Assembly.GetExecutingAssembly();
    public static IServiceCollection RegisterDependencies(this IServiceCollection services, IConfiguration appConfiguration)
    {
        services.AddDbContext<ProfilesDbContext>(opt =>
        {
            opt.UseNpgsql(appConfiguration.GetConnectionString("DefaultConnection"));
        });
        services.AddScoped<IMigrationsManager, MigrationsManager>();
        services.AddValidatorsFromAssembly(ApplicationAssembly);
        services.AddAutoMapper(ApplicationAssembly);
        services.AddScoped<IRepository, Repository>();
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = appConfiguration.GetConnectionString("Redis");
            options.InstanceName = "IdentityService";
        });
        services.RegisterDomainServices();
        return services;
    }
    
    private static IServiceCollection RegisterDomainServices(this IServiceCollection services)
    {
        services.AddScoped<ILocationService, LocationService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IProfileService, ProfileService>();
        
        return services;
    }
}