using System.Reflection;
using ChatService.Application.Services;
using ChatService.Database;
using ChatService.Database.AutoMigrations;
using ChatService.Database.Repository;
using ChatService.Domain.Contracts;
using Core.Database;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace ChatService.Setup;

public static class DependencyInjection
{
    public static void AddDependencies(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSignalR();
        services.AddDbContext<ChatDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")).LogTo(Log.Logger.Information, LogLevel.Information);
        });
        services.AddScoped<IMigrationsManager, MigrationsManager>();
        services.AddScoped<IChatService, ChatService.Application.Services.ChatService>();
        services.AddScoped<IRepository,Repository>();
        services.AddScoped<IUserService, UserServices>();
        services.AddCors(opt =>
        {
            opt.AddDefaultPolicy(builder => builder.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod().AllowCredentials());
        });
        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();
            x.AddConsumers(Assembly.GetExecutingAssembly());
            x.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host("rabbitmq", "/", h => { });
                
                configurator.ConfigureEndpoints(context);
            });
        });
    }
}