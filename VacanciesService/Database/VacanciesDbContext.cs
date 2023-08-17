using Microsoft.EntityFrameworkCore;
using VacanciesService.Domain.Models;

namespace VacanciesService.Database;

public class VacanciesDbContext : DbContext
{
    public VacanciesDbContext(DbContextOptions<VacanciesDbContext> options) : base(options)
    {
        // timestamp problem 
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    
    public DbSet<Vacancy> Vacancy { get; set; }
    public DbSet<Location> Location { get; set; }
    public DbSet<Skill> Skill { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<Company> Company { get; set; }
    public DbSet<LocationVacancy> LocationVacancy { get; set; }
    public DbSet<VacancySkill> VacancySkill { get; set; }
}