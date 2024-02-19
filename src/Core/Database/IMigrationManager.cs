namespace Core.Database;

public interface IMigrationsManager
{
    Task MigrateDbIfNeeded();
}