﻿using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text;
using Core.Database;
using Core.Validation;
using FluentValidation;
using IdentityService.Application.Services;
using IdentityService.Database;
using IdentityService.Database.AutoMigrations;
using IdentityService.Database.Repository;
using IdentityService.Domain.Contracts;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace IdentityService.Setup;

public static class DependencyInjection
{
    private static Assembly ApplicationAssembly => Assembly.GetExecutingAssembly();
    public static IServiceCollection RegisterDependencies(this IServiceCollection services, IConfiguration appConfiguration)
    {
        services.AddDbContext<IdentityDbContext>(opt =>
        {
            opt.UseNpgsql(appConfiguration.GetConnectionString("IdentityPostgres")).LogTo(Log.Logger.Information, LogLevel.Information);
        });
        services.AddScoped<IMigrationsManager, MigrationsManager>();
        services.AddValidatorsFromAssembly(ApplicationAssembly);
        services.AddFluentValidationAutoValidation(opt =>
        {
            opt.OverrideDefaultResultFactoryWith<FluentValidationAutoValidationCustomResultFactory>();
        });
        services.AddScoped<UserManager>();
        services.AddScoped<IRepository, Repository>();
        services.AddScoped<IJWTService, JWTService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = appConfiguration.GetConnectionString("Redis");
            options.InstanceName = "IdentityService";
        });
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
            x.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(appConfiguration.GetConnectionString("RabbitMq"));
                
                configurator.ConfigureEndpoints(context);
            });
        });
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        return services;
    }
}