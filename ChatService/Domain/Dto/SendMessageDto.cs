namespace ChatService.Domain.Dto;

public record SendMessageDto
{
    public string Content { get; set; }
    public Guid SenderId { get; set; }
    public string SenderName { get; set; }
    public Guid ReceiverId { get; set; }
    public Guid ChatId { get; set; }
};