using System.ComponentModel.DataAnnotations.Schema;

namespace ChatService.Domain.Models;

public class Message
{
    public Guid Id { get; init; }
    public string Content { get; init; } = null!;
    public DateTime TimeStamp { get; init; }
    [ForeignKey("Sender")]
    public Guid SenderId { get; init; }
    [ForeignKey("Receiver")]
    public Guid ReceiverId { get; init; }
    [ForeignKey("Chat")]
    public Guid ChatId { get; init; }

    public bool IsRead { get; init; } = false;
    public User Receiver { get; init; }
    public User Sender { get; init; }
    public Chat Chat { get; init; }
}