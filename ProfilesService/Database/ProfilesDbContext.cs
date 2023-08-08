using Microsoft.EntityFrameworkCore;
using ProfilesService.Models;

namespace ProfilesService.Database;

public class ProfilesDbContext : DbContext
{
    public ProfilesDbContext(DbContextOptions<ProfilesDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<ProfileSkills> ProfileSkills { get; set; }
    public DbSet<LocationProfile> LocationProfiles { get; set; }
    public DbSet<User> Users { get; set; }
}