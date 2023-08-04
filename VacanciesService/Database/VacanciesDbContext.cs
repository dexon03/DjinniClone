using Microsoft.EntityFrameworkCore;

namespace VacanciesService.Database;

public class VacanciesDbContext : DbContext
{
    public VacanciesDbContext(DbContextOptions<VacanciesDbContext> options) : base(options)
    {
        
    }
}