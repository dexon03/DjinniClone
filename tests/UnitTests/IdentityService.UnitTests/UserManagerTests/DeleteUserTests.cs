using Core.Database;
using Core.Exceptions;
using Core.MessageContract;
using Helpers;
using IdentityService.Application.Services;
using IdentityService.Domain.Models;
using MassTransit;
using Moq;

namespace IdentityService.UnitTests.UserManagerTests;

public class DeleteUserTests
{
    private readonly Mock<IRepository> _repositoryMock;
    private readonly UserManager _userManager;
    private readonly Mock<IPublishEndpoint> _publishEndpointMock;

    public DeleteUserTests()
    {
        _repositoryMock = new Mock<IRepository>();
        _publishEndpointMock = new Mock<IPublishEndpoint>();
        _userManager = new UserManager(_repositoryMock.Object, _publishEndpointMock.Object);
    }

    [Fact]
    public async Task DeleteUser_UserExists_DeletesAndPublishesEvent()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User
        {
            Id = userId,
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com"
        };

        var mockDbSet = EfHelpers.CreateMockDbSet(new List<User> { user }.AsQueryable());
        _repositoryMock.Setup(r => r.GetAll<User>()).Returns(mockDbSet.Object);

        // Act
        await _userManager.DeleteUser(userId);

        // Assert
        _repositoryMock.Verify(r => r.Delete(It.Is<User>(u => u.Id == userId)), Times.Once);
        _repositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

        _publishEndpointMock.Verify(p => p.Publish(It.Is<UserDeletedEvent>(e => e.UserId == userId), default), Times.Once);
    }

    [Fact]
    public async Task DeleteUser_UserDoesNotExist_ThrowsException()
    {
        // Arrange
        var userId = Guid.NewGuid();

        var mockDbSet = EfHelpers.CreateMockDbSet(new List<User>().AsQueryable());
        _repositoryMock.Setup(r => r.GetAll<User>()).Returns(mockDbSet.Object);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ExceptionWithStatusCode>(() => _userManager.DeleteUser(userId));
        Assert.Equal($"User with id {userId} not found", exception.Message);

        _repositoryMock.Verify(r => r.Delete(It.IsAny<User>()), Times.Never);
        _repositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        _publishEndpointMock.Verify(p => p.Publish(It.IsAny<UserDeletedEvent>(), default), Times.Never);
    }
}
