namespace IdentityService.Domain.Dto;

public class JwtResponse
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public string TokenType { get; set; } = "Bearer";
    public string? Error { get; set; }
}