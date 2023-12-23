using Core.Database;
using ILogger = Serilog.ILogger;

namespace ChatService.Database.Repository;

public class Repository : BaseRepository, IRepository
{
    public Repository(ChatDbContext context, ILogger logger) : base(context, logger)
    {
        
    }
}