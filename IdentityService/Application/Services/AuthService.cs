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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using ValidationException = Core.Exceptions.ValidationException;

namespace IdentityService.Application.Services;

public class AuthService : IAuthService
{
    private readonly UserManager _userManager;
    private readonly IJWTService _jwtService;
    private readonly IRepository _repository;
    private readonly IDistributedCache _cache;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IValidator<RegisterRequest> _registerValidator;
    private readonly IValidator<LoginRequest> _loginValidator;
    private readonly IValidator<ForgotPasswordRequest> _forgotPasswordValidator;

    public AuthService(UserManager userManager, 
        IJWTService jwtService, 
        IRepository repository, 
        IDistributedCache cache,
        IPublishEndpoint publishEndpoint,
        IValidator<RegisterRequest> registerValidator,
        IValidator<LoginRequest> loginValidator,
        IValidator<ForgotPasswordRequest> forgotPasswordValidator)
    {
        _userManager = userManager;
        _jwtService = jwtService;
        _repository = repository;
        _cache = cache;
        _publishEndpoint = publishEndpoint;
        _registerValidator = registerValidator;
        _loginValidator = loginValidator;
        _forgotPasswordValidator = forgotPasswordValidator;
    }
    public async Task<JwtResponse> LoginAsync(LoginRequest request,CancellationToken cancellationToken = default)
    {
        await ValidateRequest(request, _loginValidator, cancellationToken);
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (!await IsLoginRequestValid(user, request.Password))
        {
            //TODO: replace all exception by ErrorOr
            throw new ExceptionWithStatusCode("Invalid email or password",HttpStatusCode.Unauthorized);
        }
        var token = GetNewTokenForUser(user);
        _repository.Update(user);
        await _repository.SaveChangesAsync(cancellationToken);
        await AddTokenToCache(user.Id.ToString(), token.AccessToken);
        return token;
    }


    public async Task<JwtResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
    {
        await ValidateRequest(request, _registerValidator, cancellationToken);
        
        var user = await CreateUser(request, cancellationToken);
        var token = GetNewTokenForUser(user);
        await _repository.CreateAsync(user);
        await _repository.SaveChangesAsync(cancellationToken);
        await AddTokenToCache(user.Id.ToString(), token.AccessToken);
        await _publishEndpoint.Publish(new UserCreatedEvent
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
        await ValidateRequest(request, _forgotPasswordValidator, cancellationToken);
        
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            throw new ExceptionWithStatusCode("User with this email does not exist", HttpStatusCode.NotFound);
        }

        user.PasswordHash = PasswordUtility.GetHashedPassword(request.NewPassword, user.PasswordSalt);
        _repository.Update(user);
        await _repository.SaveChangesAsync(cancellationToken);
    }

    public async Task<JwtResponse> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        var userId = _jwtService.ValidateToken(refreshToken);
        if (userId == null)
        {
            throw new ExceptionWithStatusCode("Invalid refresh token", HttpStatusCode.Unauthorized);
        }
        
        var user = await _userManager.FindByIdAsync(Guid.Parse(userId));
        var token = GetNewTokenForUser(user);
        
        await AddTokenToCache(user.Id.ToString(), token.AccessToken);
        
        return token;
    }

    private JwtResponse GetNewTokenForUser(User user)
    {
        var accessToken = _jwtService.GenerateToken(user);
        var newRefreshToken = _jwtService.GenerateRefreshToken(user);
        user.RefreshToken = newRefreshToken;

        return new JwtResponse
        {
            AccessToken = accessToken,
            RefreshToken = newRefreshToken
        };
    }
    
    private async Task<User> CreateUser(RegisterRequest request, CancellationToken cancellationToken)
    {
        var role = await _repository.GetAsync<Role>(r => r.Name == request.Role.ToString() && r.IsActive);
        if (role == null)
        {
            throw new ExceptionWithStatusCode("Role does not exist or no active", HttpStatusCode.BadRequest);
        }
        var passwordSalt = PasswordUtility.CreatePasswordSalt();
        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            PasswordSalt = passwordSalt,
            PasswordHash = PasswordUtility.GetHashedPassword(request.Password, passwordSalt),
            RoleId = role.Id,
            Role = role,
        };
        return user;
    }
    private async Task ValidateRequest<T>(T request, IValidator<T> validator, CancellationToken cancellationToken)
    {
        var result = await validator.ValidateAsync(request, cancellationToken);
        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }
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