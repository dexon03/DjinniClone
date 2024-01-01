using IdentityService.Application.Utilities;
using IdentityService.Domain.Constants;
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

        var passwordSalt = PasswordUtility.CreatePasswordSalt();
        var admin = new User
        {
            Id = Guid.NewGuid(),
            PhoneNumber = "123456789",
            FirstName = "Admin",
            LastName = "Admin",
            Email = "admin@mail.com",
            PasswordSalt = passwordSalt,
            PasswordHash = PasswordUtility.GetHashedPassword("admin", passwordSalt),
            RoleId = roles.First(x => x.Name == Roles.Admin.ToString()).Id,
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
                Id = Guid.NewGuid(),
                Name = Roles.Admin.ToString(),
            },
            new Role()
            {
                Id = Guid.NewGuid(),
                Name = Roles.Recruiter.ToString(),
            },
            new Role()
            {
                Id = Guid.NewGuid(),
                Name = Roles.Candidate.ToString(),
            },
            new Role()
            {
                Id = Guid.NewGuid(),
                Name = Roles.CompanyOwner.ToString(),
            },
        };
    }
}