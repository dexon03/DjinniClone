namespace IdentityService.Domain.Dto;

public class ForgotPasswordRequest
{
    public string Email { get; set; }
    public string NewPassword { get; set; }
}