using System.Net;
using System.Text;
using Core.Database;
using Core.Exceptions;
using Core.MessageContract;
using FluentValidation;
using IdentityService.Application.Utilities;
using IdentityService.Domain.Constants;
using IdentityService.Domain.Contracts;
using IdentityService.Domain.Dto;
using IdentityService.Domain.Models;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;

namespace IdentityService.Application.Services;

public class AuthService : IAuthService
{
    private readonly UserManager _userManager;
    private readonly IJWTService _jwtService;
    private readonly IRepository _repository;
    private readonly IDistributedCache _cache;
    private readonly IPublishEndpoint _publishEndpoint;

    public AuthService(UserManager userManager, 
        IJWTService jwtService, 
        IRepository repository, 
        IDistributedCache cache,
        IPublishEndpoint publishEndpoint)
    {
        _userManager = userManager;
        _jwtService = jwtService;
        _repository = repository;
        _cache = cache;
        _publishEndpoint = publishEndpoint;
    }
    public async Task<TokenResponse> LoginAsync(LoginRequest request,CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (!await IsLoginRequestValid(user, request.Password))
        {
            //TODO: replace all exception by ErrorOr
            throw new ExceptionWithStatusCode("Invalid email or password",HttpStatusCode.BadRequest);
        }
        var token = GetNewTokenForUser(user);
        _repository.Update(user);
        await _repository.SaveChangesAsync(cancellationToken);
        await AddTokenToCache(user.Id.ToString(), token.AccessToken);
        return token;
    }


    public async Task<TokenResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.CreateUser(request, cancellationToken);
        var token = GetNewTokenForUser(user);
        await _repository.CreateAsync(user);
        await _repository.SaveChangesAsync(cancellationToken);
        await AddTokenToCache(user.Id.ToString(), token.AccessToken);
        await _publishEndpoint.Publish<UserCreatedEvent>(new 
        {
            UserId = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            Role = user.Role.Name
        }, cancellationToken);
        return token;
    }

    public async Task ForgotPasswordAsync(ForgotPasswordRequest request, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            throw new ExceptionWithStatusCode("User with this email does not exist", HttpStatusCode.NotFound);
        }

        user.PasswordHash = PasswordUtility.GetHashedPassword(request.NewPassword, user.PasswordSalt);
        _repository.Update(user);
        await _repository.SaveChangesAsync(cancellationToken);
    }

    public async Task<TokenResponse> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        var userId = _jwtService.ValidateToken(refreshToken);
        if (userId == null)
        {
            throw new ExceptionWithStatusCode("Invalid refresh token", HttpStatusCode.BadRequest);
        }
        
        var user = await _userManager.FindByIdAsync(Guid.Parse(userId));
        var token = GetNewTokenForUser(user);
        _repository.Update(user);
        await _repository.SaveChangesAsync(cancellationToken);
        await AddTokenToCache(user.Id.ToString(), token.AccessToken);
        
        return token;
    }

    private TokenResponse GetNewTokenForUser(User user)
    {
        var accessToken = _jwtService.GenerateToken(user);
        var newRefreshToken = _jwtService.GenerateRefreshToken(user);
        user.RefreshToken = newRefreshToken;
        return new TokenResponse
        {
            AccessToken = accessToken,
            RefreshToken = newRefreshToken,
            Role = user.Role.Name,
            UserId = user.Id
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