using Microsoft.EntityFrameworkCore;

namespace IdentityService.Database;

public class IdentityDbContext : DbContext
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
    {
    }
}