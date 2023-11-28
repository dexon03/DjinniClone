using Microsoft.EntityFrameworkCore;
using ProfilesService.Domain.Models;

namespace ProfilesService.Database;

public class ProfilesDbContext : DbContext
{
    public ProfilesDbContext(DbContextOptions<ProfilesDbContext> options) : base(options)
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
    }

    public DbSet<CandidateProfile> CandidateProfile { get; set; }
    public DbSet<RecruiterProfile> RecruiterProfile { get; set; }
    public DbSet<Location> Location { get; set; }
    public DbSet<Skill> Skill { get; set; }
    public DbSet<ProfileSkills> ProfileSkills { get; set; }
    public DbSet<LocationProfile> LocationProfile { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<Company> Company { get; set; }
}