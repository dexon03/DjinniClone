using Core.Enums;

namespace Core.MessageContract;

public record UserUpdatedEvent
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; }
    public Role Role { get; set; }
}