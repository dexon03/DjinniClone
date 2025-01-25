using Core.Database;
using Core.Exceptions;
using Helpers;
using IdentityService.Application.Services;
using IdentityService.Domain.Models;
using MassTransit;
using Moq;

namespace IdentityService.UnitTests.UserManagerTests;

public class FindByIdTests
{
    private readonly Mock<IRepository> _repositoryMock;
    private readonly UserManager _userManager;

    public FindByIdTests()
    {
        _repositoryMock = new Mock<IRepository>();
        Mock<IPublishEndpoint> publishEndpointMock = new();
        _userManager = new UserManager(_repositoryMock.Object, publishEndpointMock.Object);
    }

    [Fact]
    public async Task FindByIdAsync_UserExists_ReturnsUser()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var role = new Role { Id = Guid.NewGuid(), Name = "User" };
        var user = new User
        {
            Id = userId,
            Email = "test@example.com",
            PasswordHash = "hash",
            PasswordSalt = "salt",
            FirstName = "Test",
            LastName = "User",
            PhoneNumber = "1234567890",
            Role = role,
            RoleId = role.Id
        };

        var users = new List<User> { user }.AsQueryable();

        var mockUserDbSet = EfHelpers.CreateMockDbSet(users);
        _repositoryMock.Setup(r => r.GetAll<User>()).Returns(mockUserDbSet.Object);

        // Act
        var result = await _userManager.FindByIdAsync(userId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(user.Id, result!.Id);
        Assert.Equal(user.Email, result.Email);
        Assert.Equal(user.Role.Id, result.Role.Id);
    }

    [Fact]
    public async Task FindByIdAsync_UserDoesNotExist_ThrowsException()
    {
        // Arrange
        var userId = Guid.NewGuid();

        var users = new List<User>().AsQueryable();

        var mockUserDbSet = EfHelpers.CreateMockDbSet(users);
        _repositoryMock.Setup(r => r.GetAll<User>()).Returns(mockUserDbSet.Object);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ExceptionWithStatusCode>(() => _userManager.FindByIdAsync(userId));
        Assert.Equal($"User with id {userId} not found", exception.Message);
    }
}
