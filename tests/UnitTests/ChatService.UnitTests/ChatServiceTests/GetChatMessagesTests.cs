using ChatService.Domain.Contracts;
using ChatService.Domain.Models;
using Core.Database;
using Helpers;
using Moq;

namespace ChatService.UnitTests.ChatServiceTests;

public class GetChatMessagesTests
{
    private readonly Mock<IRepository> repositoryMock;
    private readonly Mock<IUserService> userServiceMock;
    private readonly Application.Services.ChatService chatService;

    public GetChatMessagesTests()
    {
        repositoryMock = new Mock<IRepository>();
        userServiceMock = new Mock<IUserService>();
        chatService = new Application.Services.ChatService(repositoryMock.Object, userServiceMock.Object);
    }

    [Fact]
    public async Task GetChatMessages_NoMessages_ReturnsEmptyList()
    {
        // Arrange
        var chatId = Guid.NewGuid();
        var messages = new List<Message>().AsQueryable();

        var mockDbSet = EfHelpers.CreateMockDbSet(messages);
        repositoryMock.Setup(r => r.GetAll<Message>()).Returns(mockDbSet.Object);

        // Act
        var result = await chatService.GetChatMessages(chatId, CancellationToken.None);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetChatMessages_MessagesExist_ReturnsOrderedMessages()
    {
        // Arrange
        var chatId = Guid.NewGuid();
        var message1 = new Message
        {
            Id = Guid.NewGuid(),
            ChatId = chatId,
            Content = "Hello",
            TimeStamp = DateTime.Now.AddMinutes(-1),
            Sender = new User { UserName = "Sender 1" },
            Receiver = new User { UserName = "Receiver 1" },
            IsRead = true
        };

        var message2 = new Message
        {
            Id = Guid.NewGuid(),
            ChatId = chatId,
            Content = "Hi",
            TimeStamp = DateTime.Now,
            Sender = new User { UserName = "Sender 2" },
            Receiver = new User { UserName = "Receiver 2" },
            IsRead = false
        };

        var messages = new List<Message> { message1, message2 }.AsQueryable();

        var mockDbSet = EfHelpers.CreateMockDbSet(messages);
        repositoryMock.Setup(r => r.GetAll<Message>()).Returns(mockDbSet.Object);

        // Act
        var result = await chatService.GetChatMessages(chatId, CancellationToken.None);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal(message1.Content, result[0].Content);
        Assert.Equal(message2.Content, result[1].Content);
    }

    [Fact]
    public async Task GetChatMessages_ValidateDtoMapping()
    {
        // Arrange
        var chatId = Guid.NewGuid();

        var message = new Message
        {
            Id = Guid.NewGuid(),
            ChatId = chatId,
            Content = "Test Message",
            TimeStamp = DateTime.Now,
            Sender = new User { UserName = "Sender" },
            Receiver = new User { UserName = "Receiver" },
            IsRead = false
        };

        var messages = new List<Message> { message }.AsQueryable();

        var mockDbSet = EfHelpers.CreateMockDbSet(messages);
        repositoryMock.Setup(r => r.GetAll<Message>()).Returns(mockDbSet.Object);

        // Act
        var result = await chatService.GetChatMessages(chatId, CancellationToken.None);

        // Assert
        var dto = result.First();
        Assert.Equal(message.Id, dto.Id);
        Assert.Equal(message.ChatId, dto.ChatId);
        Assert.Equal(message.Content, dto.Content);
        Assert.Equal(message.TimeStamp, dto.TimeStamp);
        Assert.Equal(message.Sender, dto.Sender);
        Assert.Equal(message.Receiver, dto.Receiver);
        Assert.Equal(message.IsRead, dto.IsRead);
    }
}
