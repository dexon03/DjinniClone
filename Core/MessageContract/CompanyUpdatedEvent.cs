namespace Core.MessageContract;

public record CompanyUpdatedEvent
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}