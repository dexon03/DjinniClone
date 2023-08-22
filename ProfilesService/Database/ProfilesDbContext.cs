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
    
    public DbSet<Profile> Profile { get; set; }
    public DbSet<Location> Location { get; set; }
    public DbSet<Skill> Skill { get; set; }
    public DbSet<ProfileSkills> ProfileSkills { get; set; }
    public DbSet<LocationProfile> LocationProfile { get; set; }
    public DbSet<User> User { get; set; }
}