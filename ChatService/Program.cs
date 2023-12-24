using ChatService.Database;
using ChatService.Database.AutoMigrations;
using ChatService.Database.Repository;
using ChatService.Domain.Contracts;
using ChatService.Hubs;
using Core.Database;
using Core.Logging;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("JobService", Serilog.Events.LogEventLevel.Debug)
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", Serilog.Events.LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", Serilog.Events.LogEventLevel.Warning)
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
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")).LogTo(Log.Logger.Information, LogLevel.Information);;
});
builder.Services.AddScoped<IMigrationsManager, MigrationsManager>();
builder.Services.AddScoped<IChatService, ChatService.Application.Services.ChatService>();
builder.Services.AddScoped<IRepository,Repository>();


var app = builder.Build();

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

app.UseSerilogLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("/Chat");

app.Run();
