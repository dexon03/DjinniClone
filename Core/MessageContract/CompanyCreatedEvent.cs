namespace Core.MessageContract;

public record CompanyCreatedEvent
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}