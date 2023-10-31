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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<VacancySkill>()
            .HasKey(vs => new { vs.SkillId, vs.VacancyId });  
        modelBuilder.Entity<VacancySkill>()
            .HasOne(vs => vs.Skill)
            .WithMany(b => b.VacancySkill)
            .HasForeignKey(vs => vs.SkillId);  
        modelBuilder.Entity<VacancySkill>()
            .HasOne(vs => vs.Vacancy)
            .WithMany(c => c.VacancySkill)
            .HasForeignKey(vs => vs.VacancyId);
        
        modelBuilder.Entity<LocationVacancy>()
            .HasKey(vs => new { vs.LocationId, vs.VacancyId });
        modelBuilder.Entity<LocationVacancy>()
            .HasOne(vs => vs.Location)
            .WithMany(b => b.LocationVacancy)
            .HasForeignKey(vs => vs.LocationId);  
        modelBuilder.Entity<LocationVacancy>()
            .HasOne(vs => vs.Vacancy)
            .WithMany(c => c.LocationVacancy)
            .HasForeignKey(vs => vs.VacancyId);
    }

    public DbSet<Vacancy> Vacancy { get; set; }
    public DbSet<Location> Location { get; set; }
    public DbSet<Skill> Skill { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<Company> Company { get; set; }
    public DbSet<LocationVacancy> LocationVacancy { get; set; }
    public DbSet<VacancySkill> VacancySkill { get; set; }
}