using IdentityService.Domain.Enums;

namespace IdentityService.Domain.Dto;

public class RegisterRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public RegisterRole Role { get; set; }
}