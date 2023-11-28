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
        var locations = new[]
        {
            new Location { Id = Guid.NewGuid(), Country = "Ukraine", City = "Kyiv" },
            new Location { Id = Guid.NewGuid(), Country = "Ukraine", City = "Lviv" },
            new Location { Id = Guid.NewGuid(), Country = "Ukraine", City = "Kharkiv" },
            new Location { Id = Guid.NewGuid(), Country = "Ukraine", City = "Dnipro" },
            new Location { Id = Guid.NewGuid(), Country = "Ukraine", City = "Odesa" },
            new Location { Id = Guid.NewGuid(), Country = "Ukraine", City = "Zaporizhzhia" },
            new Location { Id = Guid.NewGuid(), Country = "Ukraine", City = "Vinnytsia" },
            new Location { Id = Guid.NewGuid(), Country = "Ukraine", City = "Khmelnytskyi" },
        };

        var skills = new[]
        {
            new Skill { Id = Guid.NewGuid(), Name = "C#" },
            new Skill { Id = Guid.NewGuid(), Name = "Java" },
            new Skill { Id = Guid.NewGuid(), Name = "Python" },
            new Skill { Id = Guid.NewGuid(), Name = "JavaScript" },
            new Skill { Id = Guid.NewGuid(), Name = "C++" },
            new Skill { Id = Guid.NewGuid(), Name = "PHP" },
            new Skill { Id = Guid.NewGuid(), Name = "Ruby" },
            new Skill { Id = Guid.NewGuid(), Name = "Swift" },
            new Skill { Id = Guid.NewGuid(), Name = "Go" },
            new Skill { Id = Guid.NewGuid(), Name = "Kotlin" },
            new Skill { Id = Guid.NewGuid(), Name = "TypeScript" },
            new Skill { Id = Guid.NewGuid(), Name = "Scala" },
        };
        
        modelBuilder.Entity<Location>().HasData(locations);
        modelBuilder.Entity<Skill>().HasData(skills);
        
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