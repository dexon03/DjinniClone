using System.Linq.Expressions;
using ChatService.Domain.Contracts;
using ChatService.Domain.Dto;
using ChatService.Domain.Models;
using Core.Database;
using Moq;

namespace ChatService.UnitTests.ChatServiceTests;

public class CreateChatTests
{
    private readonly Mock<IRepository> repositoryMock;
    private readonly Mock<IUserService> userServiceMock;
    private readonly Application.Services.ChatService chatService;

    public CreateChatTests()
    {
        repositoryMock = new Mock<IRepository>();
        userServiceMock = new Mock<IUserService>();
        chatService = new Application.Services.ChatService(repositoryMock.Object, userServiceMock.Object);
    }

    [Fact]
    public async Task CreateChat_ExistentChat_AddsMessageToExistingChat()
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

        var existentChat = new Message
        {
            ChatId = Guid.NewGuid(),
            SenderId = chatDto.SenderId,
            ReceiverId = chatDto.ReceiverId
        };

        repositoryMock.Setup(r => r.FirstOrDefaultAsync<Message>(It.IsAny<Expression<Func<Message, bool>>>()))
                      .ReturnsAsync(existentChat);

        // Act
        await chatService.CreateChat(chatDto, CancellationToken.None);

        // Assert
        repositoryMock.Verify(r => r.CreateAsync(It.Is<Message>(m => 
            m.ChatId == existentChat.ChatId &&
            m.Content == chatDto.Message &&
            m.SenderId == chatDto.SenderId &&
            m.ReceiverId == chatDto.ReceiverId
        )), Times.Once);
        repositoryMock.Verify(r => r.SaveChangesAsync(CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task CreateChat_NoExistentChat_CreatesNewChatAndMessage()
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

        repositoryMock.Setup(r => r.FirstOrDefaultAsync<Message>(It.IsAny<Expression<Func<Message, bool>>>()))
                      .ReturnsAsync((Message)null!);

        // Act
        await chatService.CreateChat(chatDto, CancellationToken.None);

        // Assert
        userServiceMock.Verify(us => us.CreateUsersIfNotExists(chatDto, CancellationToken.None), Times.Once);
        repositoryMock.Verify(r => r.CreateAsync(It.Is<Chat>(c => 
            c.Name == $"{chatDto.SenderName} / {chatDto.ReceiverName}"
        )), Times.Once);
        repositoryMock.Verify(r => r.CreateAsync(It.Is<Message>(m => 
            m.Content == chatDto.Message &&
            m.SenderId == chatDto.SenderId &&
            m.ReceiverId == chatDto.ReceiverId
        )), Times.Once);
        repositoryMock.Verify(r => r.SaveChangesAsync(CancellationToken.None), Times.Once);
    }
}
