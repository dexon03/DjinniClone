using Core.Database;
using ILogger = Serilog.ILogger;

namespace VacanciesService.Database.Repository;

public class Repository : BaseRepository, IRepository
{
    public Repository(VacanciesDbContext context, ILogger logger) : base(context, logger)
    {
    }
}