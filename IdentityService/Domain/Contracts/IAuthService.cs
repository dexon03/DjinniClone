using IdentityService.Domain.Dto;

namespace IdentityService.Domain.Contracts;

public interface IAuthService
{
    Task<JwtResponse> LoginAsync(LoginRequest request);
    Task<JwtResponse> RegisterAsync(RegisterRequest request);
    Task<JwtResponse> ForgotPasswordAsync(ForgotPasswordRequest request);
    Task<JwtResponse> RefreshTokenAsync(string refreshToken);
}