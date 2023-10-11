using Core.Database;
using ILogger = Serilog.ILogger;

namespace IdentityService.Database.Repository;

public class Repository : BaseRepository, IRepository
{
    public Repository(IdentityDbContext context, ILogger logger) : base(context, logger)
    {
        
    }
}