using ChatService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatService.Database;

public class ChatDbContext : DbContext
{
    public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Message> Message { get; set; }
    public DbSet<User> User { get; set; }
}