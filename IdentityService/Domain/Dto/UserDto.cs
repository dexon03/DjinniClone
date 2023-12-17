using IdentityService.Domain.Models;

namespace IdentityService.Domain.Dto;

public record UserDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; }
    public Role? Role { get; set; }
};