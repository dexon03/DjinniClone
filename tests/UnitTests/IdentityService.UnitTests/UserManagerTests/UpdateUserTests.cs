using System.Net;
using Core.Database;
using Core.Exceptions;
using Core.MessageContract;
using Helpers;
using IdentityService.Application.Services;
using IdentityService.Domain.Dto;
using IdentityService.Domain.Models;
using MassTransit;
using Moq;

namespace IdentityService.UnitTests.UserManagerTests;

public class UpdateUserTests
{
    private readonly Mock<IRepository> _repositoryMock;
    private readonly UserManager _userManager;
    private readonly Mock<IPublishEndpoint> _publishEndpointMock;

    public UpdateUserTests()
    {
        _repositoryMock = new Mock<IRepository>();
        _publishEndpointMock = new Mock<IPublishEndpoint>();
        _userManager = new UserManager(_repositoryMock.Object, _publishEndpointMock.Object);
    }

    [Fact]
    public async Task UpdateUser_ValidRequest_UpdatesAndPublishesEvent()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var existingUser = new User
        {
            Id = userId,
            FirstName = "OldFirstName",
            LastName = "OldLastName",
            Email = "old.email@example.com",
            PhoneNumber = "1234567890",
            Role = new Role { Id = Guid.NewGuid(), Name = "Candidate" }
        };

        var updateRequest = new UpdateUserRequest
        {
            Id = userId,
            FirstName = "NewFirstName",
            LastName = "NewLastName",
            Email = "new.email@example.com",
            PhoneNumber = "0987654321",
            Role = Core.Enums.Role.Admin
        };

        var mockDbSet = EfHelpers.CreateMockDbSet(new List<User> { existingUser }.AsQueryable());
        _repositoryMock.Setup(r => r.GetAll<User>()).Returns(mockDbSet.Object);

        // Act
        var updatedUser = await _userManager.UpdateUser(updateRequest);

        // Assert
        Assert.NotNull(updatedUser);
        Assert.Equal(updateRequest.FirstName, updatedUser.FirstName);
        Assert.Equal(updateRequest.LastName, updatedUser.LastName);
        Assert.Equal(updateRequest.Email, updatedUser.Email);
        Assert.Equal(updateRequest.PhoneNumber, updatedUser.PhoneNumber);

        _repositoryMock.Verify(r => r.Update(It.IsAny<User>()), Times.Once);
        _repositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

        _publishEndpointMock.Verify(p => p.Publish(It.Is<UserUpdatedEvent>(e =>
            e.Id == userId &&
            e.FirstName == updateRequest.FirstName &&
            e.LastName == updateRequest.LastName &&
            e.Email == updateRequest.Email &&
            e.PhoneNumber == updateRequest.PhoneNumber &&
            e.Role == updateRequest.Role),
            default), Times.Once);
    }

    [Fact]
    public async Task UpdateUser_UserDoesNotExist_ThrowsException()
    {
        // Arrange
        var updateRequest = new UpdateUserRequest
        {
            Id = Guid.NewGuid(),
            FirstName = "FirstName",
            LastName = "LastName",
            Email = "email@example.com",
            PhoneNumber = "1234567890",
            Role = Core.Enums.Role.Admin
        };

        var mockDbSet = EfHelpers.CreateMockDbSet(new List<User>().AsQueryable());
        _repositoryMock.Setup(r => r.GetAll<User>()).Returns(mockDbSet.Object);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ExceptionWithStatusCode>(() => _userManager.UpdateUser(updateRequest));
        Assert.Equal($"User with id {updateRequest.Id} not found", exception.Message);

        _repositoryMock.Verify(r => r.Update(It.IsAny<User>()), Times.Never);
        _repositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        _publishEndpointMock.Verify(p => p.Publish(It.IsAny<UserUpdatedEvent>(), default), Times.Never);
    }
}
