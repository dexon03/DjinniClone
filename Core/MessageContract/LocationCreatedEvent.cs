namespace Core.MessageContract;

public record LocationCreatedEvent
{
    public Guid Id { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
};