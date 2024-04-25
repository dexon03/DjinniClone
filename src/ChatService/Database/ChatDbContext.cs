using ChatService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatService.Database;

public class ChatDbContext : DbContext
{
    public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var user = modelBuilder.Entity<User>().HasMany<Message>();
        user.WithOne(m => m.Sender).HasForeignKey(m => m.SenderId).OnDelete(DeleteBehavior.Cascade);
        user.WithOne(m => m.Receiver).HasForeignKey(m => m.ReceiverId).OnDelete(DeleteBehavior.Cascade);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Message> Message { get; set; }
    public DbSet<User> User { get; set; }
}