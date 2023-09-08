using System.Net;
using Core.Database;
using Core.Exceptions;
using IdentityService.Application.Utilities;
using IdentityService.Domain.Contracts;
using IdentityService.Domain.Dto;
using IdentityService.Domain.Models;

namespace IdentityService.Application.Services;

public class AuthService : IAuthService
{
    private readonly UserManager _userManager;
    private readonly IJWTService _jwtService;
    private readonly IRepository _repository;

    public AuthService(UserManager userManager, IJWTService jwtService, IRepository repository)
    {
        _userManager = userManager;
        _jwtService = jwtService;
        _repository = repository;
    }
    public async Task<JwtResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!isPasswordValid)
        {
            throw new Exception("Wrong password");
        }
        var token = GetNewTokenForUser(user);
        
        return token;
    }

    public async Task<JwtResponse> RegisterAsync(RegisterRequest request)
    {
        var passwordSalt = PasswordUtility.CreatePasswordSalt();
        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            PasswordSalt = passwordSalt,
            PasswordHash = PasswordUtility.GetHashedPassword(request.Password, passwordSalt),
            RoleId = request.RoleId
        };
        
        var userFromDb = await _repository.CreateAsync(user);
        await _repository.SaveChangesAsync();
        var token = GetNewTokenForUser(userFromDb);
        
        return token;
    }

    public Task<JwtResponse> ForgotPasswordAsync(ForgotPasswordRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<JwtResponse> RefreshTokenAsync(string refreshToken)
    {
        var userId = _jwtService.ValidateToken(refreshToken);
        if (userId == null)
        {
            throw new ExceptionWithStatusCode("Invalid refresh token", HttpStatusCode.Unauthorized);
        }
        
        var user = await _repository.GetByIdAsync<User>(userId);
        var token = GetNewTokenForUser(user);
        
        return token;
    }

    private JwtResponse GetNewTokenForUser(User user)
    {
        var accessToken = _jwtService.GenerateToken(user);
        var newRefreshToken = _jwtService.GenerateRefreshToken(user);
        user.RefreshToken = newRefreshToken;
        _repository.Update(user);
        _repository.SaveChanges();

        return new JwtResponse
        {
            AccessToken = accessToken,
            RefreshToken = newRefreshToken
        };
    }
}