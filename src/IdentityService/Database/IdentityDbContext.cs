using IdentityService.Application.Utilities;
using IdentityService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Database;

public class IdentityDbContext : DbContext
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var roles = GetRoles();
        modelBuilder.Entity<Role>().HasData(roles);

        var passwordSalt = "a/5Fl1UItotDXvD8sDuaBQ==";
        var admin = new User
        {
            Id = Guid.Parse("01943117-825a-7c86-b126-2e2839404e47"),
            PhoneNumber = "123456789",
            FirstName = "Admin",
            LastName = "Admin",
            Email = "admin@mail.com",
            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
            PasswordSalt = passwordSalt,
            PasswordHash = PasswordUtility.GetHashedPassword("admin", passwordSalt),
            RoleId = roles.First(x => x.Name == Core.Enums.Role.Admin.ToString()).Id,
        };
        modelBuilder.Entity<User>().HasData(admin);
    }

    public DbSet<User> User { get; set; }
    public DbSet<Role> Role { get; set; }
    
    private List<Role> GetRoles()
    {
        return new List<Role>()
        {
            new Role()
            {
                Id = Guid.Parse("01943117-fb31-746c-8c46-3e4656c887ba"),
                Name = Core.Enums.Role.Admin.ToString(),
            },
            new Role()
            {
                Id = Guid.Parse("01943118-4440-7b9e-8960-efa609825355"),
                Name = Core.Enums.Role.Recruiter.ToString(),
            },
            new Role()
            {
                Id = Guid.Parse("01943118-53b4-7ca3-84fc-43f969fde248"),
                Name = Core.Enums.Role.Candidate.ToString(),
            }
        };
    }
}