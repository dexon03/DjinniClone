using System.Reflection;
using ChatService.Application.Services;
using ChatService.Database;
using ChatService.Database.AutoMigrations;
using ChatService.Database.Repository;
using ChatService.Domain.Contracts;
using ChatService.Hubs;
using Core.Database;
using Core.ExceptionHandler;
using Core.Logging;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("JobService", LogEventLevel.Debug)
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(builder.Configuration)
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .CreateLogger();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddSerilogLogging();
builder.Services.AddSignalR();
builder.Services.AddDbContext<ChatDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ChatPostgres")).LogTo(Log.Logger.Information, LogLevel.Information);;
});
builder.Services.AddScoped<IMigrationsManager, MigrationsManager>();
builder.Services.AddScoped<IChatService, ChatService.Application.Services.ChatService>();
builder.Services.AddScoped<IRepository,Repository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("front", policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
});
builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();
    x.AddConsumers(Assembly.GetExecutingAssembly());
    x.UsingRabbitMq((context, configurator) =>
    {
        configurator.Host(builder.Configuration.GetConnectionString("RabbitMq"));
                
        configurator.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

app.MapDefaultEndpoints();

using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;

    var myDependency = services.GetRequiredService<IMigrationsManager>();

    //Use the service
    myDependency?.MigrateDbIfNeeded().Wait();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("front");
app.UseSerilogLogging();

// // app.UseHttpsRedirection()

app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("/chatHub");
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.Run();
