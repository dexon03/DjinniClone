using Microsoft.EntityFrameworkCore;

namespace ProfilesService.Database;

public class ProfilesDbContext : DbContext
{
    public ProfilesDbContext(DbContextOptions<ProfilesDbContext> options) : base(options)
    {
        
    }
    
}