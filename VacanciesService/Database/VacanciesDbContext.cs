using Microsoft.EntityFrameworkCore;
using VacanciesService.Models;

namespace VacanciesService.Database;

public class VacanciesDbContext : DbContext
{
    public VacanciesDbContext(DbContextOptions<VacanciesDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Vacancy> Vacancies { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<LocationVacancy> LocationVacancies { get; set; }
    public DbSet<VacancySkill> VacancySkills { get; set; }
}