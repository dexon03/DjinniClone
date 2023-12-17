namespace Core.MessageContract;

public record UserDeletedEvent
{
    public Guid UserId { get; init; }
}