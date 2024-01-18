namespace IdentityService.Domain.Models;

public class Role
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsActive { get; set; } = true;
}