using System.Linq.Expressions;
using ChatService.Application.MessageConsumers;
using ChatService.Domain.Models;
using Core.Database;
using Core.MessageContract;
using MassTransit;
using Moq;

namespace ChatService.UnitTests.ConsumersTests;

public class UserDeletedMessageConsumerTests
{
    private readonly Mock<IRepository> repositoryMock;
    private readonly UserDeletedMessageConsumer consumer;

    public UserDeletedMessageConsumerTests()
    {
        repositoryMock = new Mock<IRepository>();
        consumer = new UserDeletedMessageConsumer(repositoryMock.Object);
    }

    [Fact]
    public async Task Consume_UserDeletedEvent_DeletesUserAndMessages()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var userDeletedEvent = new UserDeletedEvent { UserId = userId };

        var contextMock = new Mock<ConsumeContext<UserDeletedEvent>>();
        contextMock.Setup(c => c.Message).Returns(userDeletedEvent);

        // Act
        await consumer.Consume(contextMock.Object);

        // Assert
        repositoryMock.Verify(r => r.DeleteRange<Message>(It.Is<Expression<Func<Message, bool>>>(
            expr => expr.Compile().Invoke(new Message { ReceiverId = userId }) &&
                    expr.Compile().Invoke(new Message { SenderId = userId })
        )), Times.Once);

        repositoryMock.Verify(r => r.DeleteRange<User>(It.Is<Expression<Func<User, bool>>>(
            expr => expr.Compile().Invoke(new User { Id = userId })
        )), Times.Once);

        repositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
