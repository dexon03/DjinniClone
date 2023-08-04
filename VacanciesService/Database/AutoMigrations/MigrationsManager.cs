using Microsoft.EntityFrameworkCore;
using Serilog;
using ILogger = Serilog.ILogger;

namespace VacanciesService.Database.AutoMigrations;

public interface IMigrationsManager
{
    Task MigrateDbIfNeeded();
}

public class MigrationsManager : IMigrationsManager
{
    private readonly VacanciesDbContext _context;
    private readonly ILogger _log = Log.ForContext<MigrationsManager>();

    public MigrationsManager(VacanciesDbContext context)
    {
        _context = context;
    }
    
    public async Task MigrateDbIfNeeded()
    {
        var dbContextName = _context.GetType().Name;
        _log.Information($"Getting pending migrations for {dbContextName}");
        var migrations = await _context.Database.GetPendingMigrationsAsync();
        var pendingMigrations = migrations.ToArray();
        if (pendingMigrations.Any())
        {
            _log.Information($"Migrating database for {dbContextName}");
            try
            {
                await _context.Database.MigrateAsync();
                _log.Information($"Successfully finished migrating database for {dbContextName}");
            }
            catch (Exception e)
            {
                _log.Error(e, $"Error while executing migration for context {dbContextName}");
            }
        }
        
    }
}