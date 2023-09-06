using IdentityService.Domain.Contracts;
using IdentityService.Domain.Dto;
using Microsoft.AspNetCore.Authentication;

namespace IdentityService.Application.Services;

public class AuthService : IAuthService
{
    private readonly UserManager _userManager;
    private readonly IJWTService _jwtService;

    public AuthService(UserManager userManager, IJWTService jwtService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
    }
    public async Task<JwtResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!isPasswordValid)
        {
            throw new Exception("Wrong password");
        }

        return new JwtResponse
        {
            AccessToken = _jwtService.GenerateToken(user),
            RefreshToken = _jwtService.GenerateRefreshToken(user)
        };
    }

    public Task<JwtResponse> RegisterAsync(RegisterRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<JwtResponse> ForgotPasswordAsync(ForgotPasswordRequest request)
    {
        throw new NotImplementedException();
    }
}