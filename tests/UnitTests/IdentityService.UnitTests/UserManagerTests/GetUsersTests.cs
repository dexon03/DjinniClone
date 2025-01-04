using Core.Database;
using Helpers;
using IdentityService.Application.Services;
using IdentityService.Domain.Models;
using MassTransit;
using Moq;

namespace IdentityService.UnitTests.UserManagerTests;

public class GetUsersTests
{
    private readonly Mock<IRepository> _repositoryMock;
    private readonly UserManager _userManager;

    public GetUsersTests()
    {
        _repositoryMock = new Mock<IRepository>();
        Mock<IPublishEndpoint> publishEndpointMock = new();
        _userManager = new UserManager(_repositoryMock.Object, publishEndpointMock.Object);
    }

    [Fact]
    public async Task GetUsers_NoUsers_ReturnsEmptyList()
    {
        // Arrange
        var users = new List<User>().AsQueryable();

        var mockDbSet = EfHelpers.CreateMockDbSet(users);
        _repositoryMock.Setup(r => r.GetAll<User>()).Returns(mockDbSet.Object);

        // Act
        var result = await _userManager.GetUsers(1, 10);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetUsers_UsersExist_ReturnsPagedUsers()
    {
        // Arrange
        var role = new Role { Id = Guid.NewGuid(), Name = "Admin" };
        var users = new List<User>
        {
            new User
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                CreatedAt = DateTime.Now.AddDays(-1),
                Role = role,
                RoleId = role.Id
            },
            new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane.doe@example.com",
                CreatedAt = DateTime.Now,
                Role = role,
                RoleId = role.Id
            }
        }.AsQueryable();

        var mockDbSet = EfHelpers.CreateMockDbSet(users);
        _repositoryMock.Setup(r => r.GetAll<User>()).Returns(mockDbSet.Object);

        // Act
        var result = await _userManager.GetUsers(1, 1);

        // Assert
        Assert.Single(result);
        Assert.Equal("John", result.First().FirstName);
    }

    [Fact]
    public async Task GetUsers_ValidatesDtoMapping()
    {
        // Arrange
        var role = new Role { Id = Guid.NewGuid(), Name = "User" };
        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Test",
            LastName = "User",
            Email = "test.user@example.com",
            CreatedAt = DateTime.Now,
            Role = role,
            RoleId = role.Id
        };

        var users = new List<User> { user }.AsQueryable();
        var mockDbSet = EfHelpers.CreateMockDbSet(users);
        _repositoryMock.Setup(r => r.GetAll<User>()).Returns(mockDbSet.Object);

        // Act
        var result = await _userManager.GetUsers(1, 10);

        // Assert
        var retrievedUser = result.First();
        Assert.Equal(user.Id, retrievedUser.Id);
        Assert.Equal(user.FirstName, retrievedUser.FirstName);
        Assert.Equal(user.LastName, retrievedUser.LastName);
        Assert.Equal(user.Email, retrievedUser.Email);
        Assert.Equal(user.Role.Id, retrievedUser.Role!.Id);
    }
}
