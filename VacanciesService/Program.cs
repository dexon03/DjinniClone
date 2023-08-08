using System.Reflection;
using Core.ExceptionHandler;
using Core.Logging;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Serilog;
using VacanciesService.Database;
using VacanciesService.Database.AutoMigrations;

var builder = WebApplication.CreateBuilder(args);

var assembly = Assembly.GetExecutingAssembly();

// Add services to the container.
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .WriteTo.Console()
    .CreateLogger();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddSerilogLogging();
builder.Services.AddDbContext<VacanciesDbContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IMigrationsManager, MigrationsManager>();
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(assembly);
});
builder.Services.AddAutoMapper(assembly);
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

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.Run();