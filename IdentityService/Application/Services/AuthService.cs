using System.Net;
using System.Text;
using Core.Database;
using Core.Exceptions;
using IdentityService.Application.Utilities;
using IdentityService.Domain.Constants;
using IdentityService.Domain.Contracts;
using IdentityService.Domain.Dto;
using IdentityService.Domain.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace IdentityService.Application.Services;

public class AuthService : IAuthService
{
    private readonly UserManager _userManager;
    private readonly IJWTService _jwtService;
    private readonly IRepository _repository;
    private readonly IDistributedCache _cache;

    public AuthService(UserManager userManager, IJWTService jwtService, IRepository repository, IDistributedCache cache)
    {
        _userManager = userManager;
        _jwtService = jwtService;
        _repository = repository;
        _cache = cache;
    }
    public async Task<JwtResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (!await IsLoginRequestValid(user, request.Password))
        {
            //TODO: replace all exception by ErrorOr
            throw new ExceptionWithStatusCode("Invalid email or password",HttpStatusCode.Unauthorized);
        }
        var token = GetNewTokenForUser(user);
        
        await AddTokenToCache(user.Id.ToString(), token.AccessToken);
        
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
        
        await AddTokenToCache(user.Id.ToString(), token.AccessToken);
        
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
        
        await AddTokenToCache(user.Id.ToString(), token.AccessToken);
        
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
    
    private async Task<bool> IsLoginRequestValid(User? user, string password)
    {
        if (user == null)
        {
            return false;
        }
        return await _userManager.CheckPasswordAsync(user, password);
    }

    private async Task AddTokenToCache(string userId, string token)
    {
        var tokenBytes = Encoding.UTF8.GetBytes(token);
        await _cache.SetAsync(userId, tokenBytes, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(JwtConstants.TokenExpirationTimeInHours)
        });
    }
}