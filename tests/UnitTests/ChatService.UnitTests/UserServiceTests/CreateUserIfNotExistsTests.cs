using ChatService.Application.Services;
using ChatService.Domain.Dto;
using ChatService.Domain.Models;
using Core.Database;
using Moq;

namespace ChatService.UnitTests.UserServiceTests;

public class CreateUserIfNotExistsTests
{
    private readonly Mock<IRepository> _repositoryMock;
    private readonly UserService _userService;

    public CreateUserIfNotExistsTests()
    {
        _repositoryMock = new Mock<IRepository>();
        _userService = new UserService(_repositoryMock.Object);
    }

    [Fact]
    public async Task CreateUsersIfNotExists_BothUsersExist_DoesNotCreateNewUsers()
    {
        // Arrange
        var chatDto = new CreateChatDto
        {
            SenderId = Guid.NewGuid(),
            SenderName = "Sender",
            ReceiverId = Guid.NewGuid(),
            ReceiverName = "Receiver",
            Message = "Hello"
        };

        _repositoryMock.Setup(r => r.AnyAsync<User>(u => u.Id == chatDto.SenderId))
                       .ReturnsAsync(true);
        _repositoryMock.Setup(r => r.AnyAsync<User>(u => u.Id == chatDto.ReceiverId))
                       .ReturnsAsync(true);

        // Act
        await _userService.CreateUsersIfNotExists(chatDto, CancellationToken.None);

        // Assert
        _repositoryMock.Verify(r => r.CreateAsync(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public async Task CreateUsersIfNotExists_NeitherUserExists_CreatesBothUsers()
    {
        // Arrange
        var chatDto = new CreateChatDto
        {
            SenderId = Guid.NewGuid(),
            SenderName = "Sender",
            ReceiverId = Guid.NewGuid(),
            ReceiverName = "Receiver",
            Message = "Hello"
        };

        _repositoryMock.Setup(r => r.AnyAsync<User>(u => u.Id == chatDto.SenderId))
                       .ReturnsAsync(false);
        _repositoryMock.Setup(r => r.AnyAsync<User>(u => u.Id == chatDto.ReceiverId))
                       .ReturnsAsync(false);

        // Act
        await _userService.CreateUsersIfNotExists(chatDto, CancellationToken.None);

        // Assert
        _repositoryMock.Verify(r => r.CreateAsync(It.Is<User>(u => u.Id == chatDto.SenderId && u.UserName == chatDto.SenderName)), Times.Once);
        _repositoryMock.Verify(r => r.CreateAsync(It.Is<User>(u => u.Id == chatDto.ReceiverId && u.UserName == chatDto.ReceiverName)), Times.Once);
    }

    [Fact]
    public async Task CreateUsersIfNotExists_OnlySenderExists_CreatesReceiver()
    {
        // Arrange
        var chatDto = new CreateChatDto
        {
            SenderId = Guid.NewGuid(),
            SenderName = "Sender",
            ReceiverId = Guid.NewGuid(),
            ReceiverName = "Receiver",
            Message = "Hello"
        };

        _repositoryMock.Setup(r => r.AnyAsync<User>(u => u.Id == chatDto.SenderId))
                       .ReturnsAsync(true);
        _repositoryMock.Setup(r => r.AnyAsync<User>(u => u.Id == chatDto.ReceiverId))
                       .ReturnsAsync(false);

        // Act
        await _userService.CreateUsersIfNotExists(chatDto, CancellationToken.None);

        // Assert
        _repositoryMock.Verify(r => r.CreateAsync(It.Is<User>(u => u.Id == chatDto.ReceiverId && u.UserName == chatDto.ReceiverName)), Times.Once);
        _repositoryMock.Verify(r => r.CreateAsync(It.Is<User>(u => u.Id == chatDto.SenderId)), Times.Never);
    }

    [Fact]
    public async Task CreateUsersIfNotExists_OnlyReceiverExists_CreatesSender()
    {
        // Arrange
        var chatDto = new CreateChatDto
        {
            SenderId = Guid.NewGuid(),
            SenderName = "Sender",
            ReceiverId = Guid.NewGuid(),
            ReceiverName = "Receiver",
            Message = "Hello"
        };

        _repositoryMock.Setup(r => r.AnyAsync<User>(u => u.Id == chatDto.SenderId))
                       .ReturnsAsync(false);
        _repositoryMock.Setup(r => r.AnyAsync<User>(u => u.Id == chatDto.ReceiverId))
                       .ReturnsAsync(true);

        // Act
        await _userService.CreateUsersIfNotExists(chatDto, CancellationToken.None);

        // Assert
        _repositoryMock.Verify(r => r.CreateAsync(It.Is<User>(u => u.Id == chatDto.SenderId && u.UserName == chatDto.SenderName)), Times.Once);
        _repositoryMock.Verify(r => r.CreateAsync(It.Is<User>(u => u.Id == chatDto.ReceiverId)), Times.Never);
    }
}
