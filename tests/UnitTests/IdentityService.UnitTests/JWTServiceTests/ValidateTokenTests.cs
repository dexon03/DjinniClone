using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using Core.Database;
using IdentityService.Application.Services;
using IdentityService.Domain.Models;
using Microsoft.Extensions.Configuration;
using Moq;

namespace IdentityService.UnitTests.JWTServiceTests;

public class ValidateTokenTests
{
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly Mock<IRepository> _repositoryMock;
    private readonly JWTService _jwtService;

    public ValidateTokenTests()
    {
        _configurationMock = new Mock<IConfiguration>();
        _repositoryMock = new Mock<IRepository>();
        _jwtService = new JWTService(_configurationMock.Object, _repositoryMock.Object);
    }

    [Fact]
    public void ValidateToken_ValidToken_ReturnsUserId()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var refreshTokenKey = "F4XCU4J99BFRRK1OHHS4WMG3ZHHBV4Y7RFY9E3QDASDGHDASSDFH";

        _configurationMock.Setup(c => c["JWT:RefreshTokenKey"]).Returns(refreshTokenKey);
        _configurationMock.Setup(c => c["Jwt:RefreshTokenKey"]).Returns(refreshTokenKey);
        _configurationMock.Setup(c => c["Jwt:Issuer"]).Returns("https://myapp.com");
        _configurationMock.Setup(c => c["Jwt:Audience"]).Returns("https://myapp.com");
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        _repositoryMock.Setup(r => r.Any<User>(It.IsAny<Expression<Func<User, bool>>>())).Returns(true);

        // Act
        var token = _jwtService.GenerateRefreshToken(new User { Id = userId });
        var result = _jwtService.ValidateToken(token);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userId.ToString(), result);
    }

    [Fact]
    public void ValidateToken_InvalidToken_ReturnsNull()
    {
        // Arrange
        var token = "invalidJwtToken";
        var refreshTokenKey = "F4XCU4J99BFRRK1OHHS4WMG3ZHHBV4Y7RFY9E3QDASDGHDASSDFH";

        _configurationMock.Setup(c => c["JWT:RefreshTokenKey"]).Returns(refreshTokenKey);
        _configurationMock.Setup(c => c["Jwt:Issuer"]).Returns("https://myapp.com");
        _configurationMock.Setup(c => c["Jwt:Audience"]).Returns("https://myapp.com");

        // Act
        var result = _jwtService.ValidateToken(token);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ValidateToken_TokenWithMissingConfiguration_ThrowsException()
    {
        // Arrange
        var token = "validJwtToken";

        _configurationMock.Setup(c => c["JWT:RefreshTokenKey"]).Returns((string)null);

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => _jwtService.ValidateToken(token));
        Assert.Equal("JWT Refresh Token Key is not set", exception.Message);
    }

    [Fact]
    public void ValidateToken_RefreshTokenNotExists_ReturnsNull()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();
        var token = "validJwtToken";
        var refreshTokenKey = "F4XCU4J99BFRRK1OHHS4WMG3ZHHBV4Y7RFY9E3QDASDGHDASSDFH";

        _configurationMock.Setup(c => c["JWT:RefreshTokenKey"]).Returns(refreshTokenKey);
        _configurationMock.Setup(c => c["Jwt:Issuer"]).Returns("https://myapp.com");
        _configurationMock.Setup(c => c["Jwt:Audience"]).Returns("https://myapp.com");

        _repositoryMock.Setup(r => r.Any<User>(It.IsAny<Expression<Func<User, bool>>>())).Returns(false);

        // Act
        var result = _jwtService.ValidateToken(token);

        // Assert
        Assert.Null(result);
    }
}
