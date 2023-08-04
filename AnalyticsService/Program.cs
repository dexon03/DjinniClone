using AnalyticsService.Database;
using AnalyticsService.Database.AutoMigrations;
using Core.Logging;
using Microsoft.EntityFrameworkCore;
using Serilog;
using IMigrationsManager = AnalyticsService.Database.AutoMigrations.IMigrationsManager;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .WriteTo.Console()
    .CreateLogger();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .WriteTo.Console()
    .CreateLogger();
builder.Services.AddSingleton(Log.Logger);
builder.Host.UseSerilog();
builder.Services.AddDbContext<AnalyticsDbContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IMigrationsManager, MigrationsManager>();
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

app.UseAuthorization();

app.MapControllers();

app.Run();