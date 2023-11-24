using System.Reflection;
using System.Text;
using Core.Database;
using FluentValidation;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProfilesService.Application.Services;
using ProfilesService.Database;
using ProfilesService.Database.AutoMigrations;
using ProfilesService.Database.Repository;
using ProfilesService.Domain.Contracts;
using Serilog;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace ProfilesService.Setup;

public static class DependencyInjection
{
    private static Assembly _applicationAssembly = Assembly.GetExecutingAssembly();
    public static IServiceCollection RegisterDependencies(this IServiceCollection services, IConfiguration appConfiguration)
    {
        services.AddDbContext<ProfilesDbContext>(opt =>
        {
            opt.UseNpgsql(appConfiguration.GetConnectionString("DefaultConnection")).LogTo(Log.Logger.Information, LogLevel.Information);
        });
        services.AddScoped<IMigrationsManager, MigrationsManager>();
        services.AddValidatorsFromAssembly(_applicationAssembly);
        services.AddFluentValidationAutoValidation();
        services.AddAutoMapper(_applicationAssembly);
        services.AddScoped<IRepository, Repository>();
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = appConfiguration.GetConnectionString("Redis");
            options.InstanceName = "IdentityService";
        });
        services.RegisterDomainServices();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = appConfiguration["Jwt:Issuer"],
                    ValidAudience = appConfiguration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appConfiguration["Jwt:Key"]))
                };
            });
        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();
            x.AddConsumers(_applicationAssembly);
            x.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host("rabbitmq", "/", h => { });
                
                configurator.ConfigureEndpoints(context);
            });
        });
        services.AddMassTransitHostedService();
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