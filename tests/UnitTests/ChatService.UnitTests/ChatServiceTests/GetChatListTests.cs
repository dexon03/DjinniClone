using ChatService.Domain.Contracts;
using ChatService.Domain.Models;
using Core.Database;
using Helpers;
using Moq;

namespace ChatService.UnitTests.ChatServiceTests;

public class GetChatListTests
{
    private readonly Mock<IRepository> repositoryMock;
    private readonly Mock<IUserService> userServiceMock;
    private readonly Application.Services.ChatService chatService;

    public GetChatListTests()
    {
        repositoryMock = new Mock<IRepository>();
        userServiceMock = new Mock<IUserService>();
        chatService = new Application.Services.ChatService(repositoryMock.Object, userServiceMock.Object);
    }

    [Fact]
    public async Task GetChatList_UserHasNoChats_ReturnsEmptyList()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var messages = new List<Message>().AsQueryable();

        var mockDbSet = EfHelpers.CreateMockDbSet(messages);
        repositoryMock.Setup(r => r.GetAll<Message>()).Returns(mockDbSet.Object);

        // Act
        var result = await chatService.GetChatList(userId, 1, 10, CancellationToken.None);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetChatList_UserHasChats_ReturnsCorrectChatList()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var chatId = Guid.NewGuid();

        var messages = new List<Message>
        {
            new Message
            {
                Chat = new Chat { Id = chatId, Name = "Chat Name" },
                Sender = new User { UserName = "Sender" },
                Content = "Last Message",
                TimeStamp = DateTime.Now,
                ReceiverId = userId,
                IsRead = false
            }
        }.AsQueryable();

        var mockDbSet = EfHelpers.CreateMockDbSet(messages);
        repositoryMock.Setup(r => r.GetAll<Message>()).Returns(mockDbSet.Object);

        // Act
        var result = await chatService.GetChatList(userId, 1, 10, CancellationToken.None);

        // Assert
        Assert.Single(result);
        Assert.Equal(chatId, result.First().Id);
        Assert.Equal("Chat Name", result.First().Name);
        Assert.Equal("Sender", result.First().SenderOfLastMessage);
        Assert.Equal("Last Message", result.First().LastMessage);
        Assert.False(result.First().IsLastMessageRead);
    }

    [Fact]
    public async Task GetChatList_UserHasMultipleChats_ReturnsPaginatedList()
    {
        // Arrange
        var userId = Guid.NewGuid();

        var messages = new List<Message>
        {
            new Message
            {
                Chat = new Chat { Id = Guid.NewGuid(), Name = "Chat 1" },
                Sender = new User { UserName = "Sender 1" },
                Content = "Message 1",
                TimeStamp = DateTime.Now.AddMinutes(-1),
                ReceiverId = userId,
                IsRead = true
            },
            new Message
            {
                Chat = new Chat { Id = Guid.NewGuid(), Name = "Chat 2" },
                Sender = new User { UserName = "Sender 2" },
                Content = "Message 2",
                TimeStamp = DateTime.Now,
                ReceiverId = userId,
                IsRead = false
            }
        }.AsQueryable();

        var mockDbSet = EfHelpers.CreateMockDbSet(messages);
        repositoryMock.Setup(r => r.GetAll<Message>()).Returns(mockDbSet.Object);

        // Act
        var result = await chatService.GetChatList(userId, 1, 1, CancellationToken.None);

        // Assert
        Assert.Single(result);
        Assert.Equal("Chat 2", result.First().Name);
    }
}
