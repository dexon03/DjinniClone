using System.Reflection;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using VacanciesService.Database;
using VacanciesService.Database.AutoMigrations;
using VacanciesService.Database.Repository;
using VacanciesService.Domain.Contracts;

namespace VacanciesService.Setup;

public static class DependencyInjection
{
    private static Assembly ApplicationAssembly => Assembly.GetExecutingAssembly();
    public static IServiceCollection RegisterDependencies(this IServiceCollection services, IConfiguration appConfiguration)
    {
        services.AddDbContext<VacanciesDbContext>(opt => opt.UseNpgsql(appConfiguration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IMigrationsManager, MigrationsManager>();
        services.AddValidatorsFromAssembly(ApplicationAssembly);
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(ApplicationAssembly);
        });
        services.AddAutoMapper(ApplicationAssembly);
        services.AddScoped<IVacancyRepository, VacancyRepository>();
        return services;
    }
}