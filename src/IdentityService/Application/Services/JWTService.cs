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

public class JWTService(IConfiguration configuration, IRepository repository) : IJWTService
{
    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key is not set"));
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
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"] 
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string GenerateRefreshToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(configuration["Jwt:RefreshTokenKey"] ?? throw new InvalidOperationException("JWT Refresh Token Key is not set"));
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            }),
            Expires = DateTime.UtcNow.AddHours(JwtConstants.RefreshTokenExpirationTimeInHours),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"] 
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

        if (!isTokenValid)
        {
            return null;
        }

        return userId;
    }

    private bool TryValidateToken(string token, out ClaimsPrincipal principal)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(configuration["JWT:RefreshTokenKey"] ?? throw new InvalidOperationException("JWT Refresh Token Key is not set"));

        try
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = configuration["Jwt:Audience"],
                ValidIssuer = configuration["Jwt:Issuer"],
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
}