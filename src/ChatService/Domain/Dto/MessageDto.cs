using ChatService.Domain.Models;

namespace ChatService.Domain.Dto;

public record MessageDto
{
    public Guid Id { get; set; }
    public string Content { get; set; } = null!;
    public DateTime TimeStamp { get; init; }
    public User Sender { get; set; }
    public User Receiver { get; set; }
    public Guid ChatId { get; set; }
    public bool IsRead { get; set; }
};