namespace IdentityService.Domain.Dto;

public class TokenResponse
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public string Role { get; set; }
    public Guid UserId { get; set; }
}