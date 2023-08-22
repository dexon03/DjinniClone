using Core.Database;
using ILogger = Serilog.ILogger;

namespace ProfilesService.Database.Repository;

public class Repository : BaseRepository, IRepository
{
    public Repository(ProfilesDbContext context, ILogger logger) : base(context, logger)
    {
    }
}