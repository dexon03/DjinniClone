using System.Reflection;
using Core.Database;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            opt.UseNpgsql(appConfiguration.GetConnectionString("DefaultConnection"));
        });
        services.AddScoped<IMigrationsManager, MigrationsManager>();
        services.AddValidatorsFromAssembly(ApplicationAssembly);
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(ApplicationAssembly);
        });
        services.AddAutoMapper(ApplicationAssembly);
        services.AddScoped<IRepository, Repository>();
        services.RegisterDomainServices();
        return services;
    }
    
    private static IServiceCollection RegisterDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IVacanciesService, VacancyService>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ILocationService, LocationService>();
        return services;
    }
}