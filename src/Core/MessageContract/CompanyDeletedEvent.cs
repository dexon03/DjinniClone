namespace Core.MessageContract;

public record CompanyDeletedEvent
{
    public Guid Id { get; set; }
};