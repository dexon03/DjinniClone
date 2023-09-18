using IdentityService.Domain.Dto;

namespace IdentityService.Domain.Contracts;

public interface IAuthService
{
    Task<JwtResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
    Task<JwtResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default);
    Task<JwtResponse> ForgotPasswordAsync(ForgotPasswordRequest request, CancellationToken cancellationToken = default);
    Task<JwtResponse> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
}