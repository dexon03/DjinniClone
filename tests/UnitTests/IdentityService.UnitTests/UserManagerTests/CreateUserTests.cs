using System.Linq.Expressions;
using System.Net;
using Core.Database;
using Core.Exceptions;
using IdentityService.Application.Services;
using IdentityService.Domain.Dto;
using IdentityService.Domain.Models;
using MassTransit;
using Moq;

namespace IdentityService.UnitTests.UserManagerTests;

public class CreateUserTests
{
    private readonly Mock<IRepository> _repositoryMock;
    private readonly UserManager _userManager;

    public CreateUserTests()
    {
        _repositoryMock = new Mock<IRepository>();
        Mock<IPublishEndpoint> publishEndpointMock = new();
        _userManager = new UserManager(_repositoryMock.Object, publishEndpointMock.Object);
    }

    [Fact]
    public async Task CreateUser_ValidRequest_ReturnsUser()
    {
        // Arrange
        var role = new Role { Id = Guid.NewGuid(), Name = "Admin", IsActive = true };
        var request = new RegisterRequest
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            PhoneNumber = "1234567890",
            Password = "password123",
            Role = Core.Enums.Role.Admin
        };

        _repositoryMock.Setup(r => r.GetAsync<Role>(It.IsAny<Expression<Func<Role, bool>>>()))
            .ReturnsAsync(role);

        // Act
        var user = await _userManager.CreateUser(request);

        // Assert
        Assert.NotNull(user);
        Assert.Equal(request.FirstName, user.FirstName);
        Assert.Equal(request.LastName, user.LastName);
        Assert.Equal(request.Email, user.Email);
        Assert.Equal(request.PhoneNumber, user.PhoneNumber);
        Assert.Equal(role.Id, user.RoleId);
        Assert.Equal(role, user.Role);
        Assert.NotNull(user.PasswordHash);
        Assert.NotNull(user.PasswordSalt);
    }

    [Fact]
    public async Task CreateUser_InvalidRole_ThrowsException()
    {
        // Arrange
        var request = new RegisterRequest
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            PhoneNumber = "1234567890",
            Password = "password123",
            Role = Core.Enums.Role.Candidate
        };

        _repositoryMock.Setup(r => r.GetAsync<Role>(It.IsAny<Expression<Func<Role, bool>>>()))!
            .ReturnsAsync((Role)null!);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ExceptionWithStatusCode>(() => _userManager.CreateUser(request));
        Assert.Equal("Role does not exist or no active", exception.Message);
        Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
    }
}

