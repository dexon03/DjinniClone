namespace IdentityService.Domain.Models;

public class RoleClaim
{
    public Guid Id { get; set; }
    public Guid RoleId { get; set; }
    public Guid ClaimId { get; set; }
}