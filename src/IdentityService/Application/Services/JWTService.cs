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
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role.Name)
            }),
            Expires = DateTime.UtcNow.AddHours(JwtConstants.TokenExpirationTimeInHours),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _configuration["Jwt:Issuer"], // Add this line
            Audience = _configuration["Jwt:Audience"] 
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string GenerateRefreshToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:RefreshTokenKey"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            }),
            Expires = DateTime.UtcNow.AddHours(JwtConstants.RefreshTokenExpirationTimeInHours),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"] 
        };

        var refreshToken = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(refreshToken);
    }
    
    public string? ValidateToken(string? token)
    {
        if (token == null)
            return null;

        var isTokenValid = TryValidateToken(token, out var principal);
        var userId = principal?.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value;

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