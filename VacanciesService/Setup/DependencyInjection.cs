using System.Reflection;
using System.Text;
using Core.Database;
using Core.Exceptions;
using Core.Validation;
using FluentValidation;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using VacanciesService.Application.Services;
using VacanciesService.Database;
using VacanciesService.Database.AutoMigrations;
using VacanciesService.Database.Repository;
using VacanciesService.Domain.Contacts;

namespace VacanciesService.Setup;

public static class DependencyInjection
{
    private static Assembly ApplicationAssembly => Assembly.GetExecutingAssembly();
    public static IServiceCollection RegisterDependencies(this IServiceCollection services, IConfiguration appConfiguration)
    {
        services.AddDbContext<VacanciesDbContext>(opt =>
        {
            opt.UseNpgsql(appConfiguration.GetConnectionString("DefaultConnection")).LogTo(Log.Logger.Information, LogLevel.Information);;
        });
        services.AddScoped<IMigrationsManager, MigrationsManager>();
        services.AddValidatorsFromAssembly(ApplicationAssembly);
        services.AddFluentValidationAutoValidation(opt =>
        {
            opt.OverrideDefaultResultFactoryWith<FluentValidationAutoValidationCustomResultFactory>();
        });
        services.AddAutoMapper(ApplicationAssembly);
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
            x.AddConsumers(ApplicationAssembly);
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
        services.AddScoped<IVacanciesService, VacancyService>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ILocationService, LocationService>();
        services.AddScoped<ISkillService, SkillService>();
        return services;
    }
}