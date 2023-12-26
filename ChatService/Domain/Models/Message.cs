using System.ComponentModel.DataAnnotations.Schema;

namespace ChatService.Domain.Models;

public class Message
{
    public Guid Id { get; set; }
    public string Content { get; set; } = null!;
    public DateTime TimeStamp { get; set; }
    [ForeignKey("Sender")]
    public Guid SenderId { get; set; }
    [ForeignKey("Receiver")]
    public Guid ReceiverId { get; set; }
    [ForeignKey("Chat")]
    public Guid ChatId { get; set; }
    public bool IsRead { get; set; }
    public User Receiver { get; set; }
    public User Sender { get; set; }
    public Chat Chat { get; set; }
}