using Core.ExceptionHandler;
using Core.Logging;
using FastEndpoints;
using IdentityService.Database;
using IdentityService.Database.AutoMigrations;
using IdentityService.Domain.Models;
using IdentityService.Setup;
using Microsoft.EntityFrameworkCore;
using Serilog;

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
    .WriteTo.Console()
    .CreateLogger();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddSerilogLogging();
builder.Services.RegisterDependencies(builder.Configuration);
builder.Services.BuildServiceProvider().GetService<IMigrationsManager>()?.MigrateDbIfNeeded().Wait();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogLogging();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseFastEndpoints();
app.MapControllers();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.Run();