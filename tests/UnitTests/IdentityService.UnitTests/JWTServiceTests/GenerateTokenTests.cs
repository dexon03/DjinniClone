using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Core.Database;
using IdentityService.Application.Services;
using IdentityService.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Moq;

namespace IdentityService.UnitTests.JWTServiceTests;

public class GenerateTokenTests
{
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly Mock<IRepository> _repositoryMock;
    private readonly JWTService _jwtService;

    public GenerateTokenTests()
    {
        _configurationMock = new Mock<IConfiguration>();
        _repositoryMock = new Mock<IRepository>();
        _jwtService = new JWTService(_configurationMock.Object, _repositoryMock.Object);
    }

    [Fact]
    public void GenerateToken_ValidUser_ReturnsToken()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = "test@example.com",
            Role = new Role { Name = "Admin" }
        };
        
        var validKey = "F4XCU4J99BFRRK1OHHS4WMG3ZHHBV4Y7RFY9E3HG";

        _configurationMock.Setup(c => c["Jwt:Key"]).Returns(validKey);
        _configurationMock.Setup(c => c["Jwt:Issuer"]).Returns("https://myapp.com");
        _configurationMock.Setup(c => c["Jwt:Audience"]).Returns("https://myapp.com");

        // Act
        var token = _jwtService.GenerateToken(user);

        // Assert
        Assert.NotNull(token);
        Assert.IsType<string>(token);

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(validKey);
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

        tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
        Assert.NotNull(validatedToken);
    }

    [Fact]
    public void GenerateToken_MissingConfiguration_ThrowsException()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = "test@example.com",
            Role = new Role { Name = "Admin" }
        };

        _configurationMock.Setup(c => c["Jwt:Key"]).Returns((string)null);

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => _jwtService.GenerateToken(user));
        Assert.Equal("JWT Key is not set", exception.Message);
    }
}

