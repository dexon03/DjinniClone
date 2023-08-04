using Microsoft.EntityFrameworkCore;

namespace AnalyticsService.Database;

public class AnalyticsDbContext : DbContext
{
    public AnalyticsDbContext(DbContextOptions<AnalyticsDbContext> options) : base(options)
    {
    }    
}