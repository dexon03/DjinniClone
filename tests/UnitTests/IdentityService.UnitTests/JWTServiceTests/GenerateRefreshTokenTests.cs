using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Core.Database;
using IdentityService.Application.Services;
using IdentityService.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Moq;

namespace IdentityService.UnitTests.JWTServiceTests;

public class GenerateRefreshTokenTests
{
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly Mock<IRepository> _repositoryMock;
    private readonly JWTService _jwtService;

    public GenerateRefreshTokenTests()
    {
        _configurationMock = new Mock<IConfiguration>();
        _repositoryMock = new Mock<IRepository>();
        _jwtService = new JWTService(_configurationMock.Object, _repositoryMock.Object);
    }

    [Fact]
    public void GenerateRefreshToken_ValidUser_ReturnsToken()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = "test@example.com",
            Role = new Role { Name = "Admin" }
        };
        
        var validRefreshTokenKey = "F4XCU4J99BFRRK1OHHS4WMG3ZHHBV4Y7RFY9E3QDASDGHDASSDFH";

        _configurationMock.Setup(c => c["Jwt:RefreshTokenKey"]).Returns(validRefreshTokenKey);
        _configurationMock.Setup(c => c["Jwt:Issuer"]).Returns("https://myapp.com");
        _configurationMock.Setup(c => c["Jwt:Audience"]).Returns("https://myapp.com");

        // Act
        var refreshToken = _jwtService.GenerateRefreshToken(user);

        // Assert
        Assert.NotNull(refreshToken);
        Assert.IsType<string>(refreshToken);

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(validRefreshTokenKey);
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://myapp.com",
            ValidAudience = "https://myapp.com",
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };

        tokenHandler.ValidateToken(refreshToken, validationParameters, out var validatedToken);
        Assert.NotNull(validatedToken);
    }

    [Fact]
    public void GenerateRefreshToken_MissingConfiguration_ThrowsException()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = "test@example.com",
            Role = new Role { Name = "Admin" }
        };

        _configurationMock.Setup(c => c["Jwt:RefreshTokenKey"]).Returns((string)null);

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => _jwtService.GenerateRefreshToken(user));
        Assert.Equal("JWT Refresh Token Key is not set", exception.Message);
    }
}
