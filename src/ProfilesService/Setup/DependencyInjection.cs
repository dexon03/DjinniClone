﻿using System.Reflection;
using System.Text;
using Core.Database;
using Core.Validation;
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
            opt.UseNpgsql(appConfiguration.GetConnectionString("ProfilePostgres")).LogTo(Log.Logger.Information, LogLevel.Information);
        });
        services.AddScoped<IMigrationsManager, MigrationsManager>();
        services.AddValidatorsFromAssembly(_applicationAssembly);
        services.AddFluentValidationAutoValidation(opt =>
        {
            opt.OverrideDefaultResultFactoryWith<FluentValidationAutoValidationCustomResultFactory>();
        });
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
                configurator.Host(appConfiguration.GetConnectionString("RabbitMq"));
                
                configurator.ConfigureEndpoints(context);
            });
        });
        
        return services;
    }
    
    private static IServiceCollection RegisterDomainServices(this IServiceCollection services)
    {
        services.AddScoped<ILocationService, LocationService>();
        services.AddScoped<IProfileService, ProfileService>();
        services.AddScoped<ISkillService, SkillService>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<IPdfService, PdfService>();
        
        return services;
    }
}