namespace IdentityService.Domain.Models;

public class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string PhoneNumber { get; set; }
    public Guid RoleId { get; set; }
    public Role Role { get; set; } = null!;
}