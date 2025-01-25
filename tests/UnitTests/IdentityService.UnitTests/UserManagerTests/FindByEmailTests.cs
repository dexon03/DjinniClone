using Core.Database;
using Helpers;
using IdentityService.Application.Services;
using IdentityService.Domain.Models;
using MassTransit;
using Moq;

namespace IdentityService.UnitTests.UserManagerTests;

public class FindByEmailTests
{
    private readonly Mock<IRepository> _repositoryMock;
    private readonly UserManager _userManager;

    public FindByEmailTests()
    {
        _repositoryMock = new Mock<IRepository>();
        Mock<IPublishEndpoint> publishEndpointMock = new();
        _userManager = new UserManager(_repositoryMock.Object, publishEndpointMock.Object);
    }

    [Fact]
    public async Task FindByEmailAsync_UserExists_ReturnsUser()
    {
        // Arrange
        var email = "test@example.com";
        var role = new Role { Id = Guid.NewGuid(), Name = "User" };
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            PasswordHash = "hash",
            PasswordSalt = "salt",
            FirstName = "Test",
            LastName = "User",
            PhoneNumber = "1234567890",
            Role = role,
            RoleId = role.Id
        };

        var users = new List<User> { user }.AsQueryable();
        var roles = new List<Role> { role }.AsQueryable();

        var mockUserDbSet = EfHelpers.CreateMockDbSet(users);
        var mockRoleDbSet = EfHelpers.CreateMockDbSet(roles);

        _repositoryMock.Setup(r => r.GetAll<User>()).Returns(mockUserDbSet.Object);
        _repositoryMock.Setup(r => r.GetAll<Role>()).Returns(mockRoleDbSet.Object);

        // Act
        var result = await _userManager.FindByEmailAsync(email);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(user.Id, result!.Id);
        Assert.Equal(user.Email, result.Email);
        Assert.Equal(user.Role.Id, result.Role!.Id);
    }

    [Fact]
    public async Task FindByEmailAsync_UserDoesNotExist_ThrowsException()
    {
        // Arrange
        var email = "nonexistent@example.com";

        var users = new List<User>().AsQueryable();
        var roles = new List<Role>().AsQueryable();

        var mockUserDbSet = EfHelpers.CreateMockDbSet(users);
        var mockRoleDbSet = EfHelpers.CreateMockDbSet(roles);

        _repositoryMock.Setup(r => r.GetAll<User>()).Returns(mockUserDbSet.Object);
        _repositoryMock.Setup(r => r.GetAll<Role>()).Returns(mockRoleDbSet.Object);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => _userManager.FindByEmailAsync(email));
        Assert.Equal("Wrong email", exception.Message);
    }
}
