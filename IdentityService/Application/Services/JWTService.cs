using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Database;
using IdentityService.Domain.Contracts;
using IdentityService.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using Claim = System.Security.Claims.Claim;
using JwtConstants = IdentityService.Domain.Constants.JwtConstants;

namespace IdentityService.Application.Services;

public class JWTService : IJWTService
{
    private readonly IConfiguration _configuration;
    private readonly IRepository _repository;

    public JWTService(IConfiguration configuration, IRepository repository)
    {
        _configuration = configuration;
        _repository = repository;
    }
    public string GenerateToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Email),
            new Claim("Role", user.Role.Name)
        };
        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddHours(JwtConstants.TokenExpirationTimeInHours),
            signingCredentials: signingCredentials);


        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken(User user)
    {
        var refreshToken = new JwtSecurityTokenHandler().CreateJwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            subject: new ClaimsIdentity(new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            }),
            expires: DateTime.UtcNow.AddHours(JwtConstants.RefreshTokenExpirationTimeInHours),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:RefreshTokenKey"]!)),
                SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(refreshToken);
    }
    
    public string? ValidateToken(string? token)
    {
        if (token == null)
            return null;

        var isTokenValid = TryValidateToken(token, out var principal);
        var userId = principal?.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;

        if (!isTokenValid || !IsRefreshTokenExistsForUser(userId, token))
        {
            return null;
        }

        return userId;
    }

    private bool TryValidateToken(string token, out ClaimsPrincipal principal)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["JWT:RefreshTokenKey"]);

        try
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = _configuration["Jwt:Audience"],
                ValidIssuer = _configuration["Jwt:Issuer"],
            };

            principal = tokenHandler.ValidateToken(token, validationParameters, out _);

            return true;
        }
        catch
        {
            principal = null;
            return false;
        }
    }

    private bool IsRefreshTokenExistsForUser(string? userId, string token)
    {
        Guid.TryParse(userId, out Guid userIdGuid);
        return _repository.Any<User>(x => x.Id == userIdGuid && x.RefreshToken == token);
    }
}