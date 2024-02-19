using IdentityService.Domain.Models;

namespace IdentityService.Domain.Contracts;

public interface IJWTService
{
    string GenerateToken(User user);
    string GenerateRefreshToken(User user);
    string? ValidateToken(string? token);
}