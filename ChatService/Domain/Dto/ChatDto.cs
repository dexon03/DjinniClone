namespace ChatService.Domain.Dto;

public class ChatDto
{
    public Guid SenderId { get; set; }
    public Guid ReceiverId { get; set; }
    public string Content { get; set; } = null!;
}