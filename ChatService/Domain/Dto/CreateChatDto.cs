namespace ChatService.Domain.Dto;

public record CreateChatDto
{
    public Guid SenderId { get; set; }
    public string SenderName { get; set; }
    public Guid ReceiverId { get; set; }
    public string ReceiverName { get; set; }
    public string Message { get; set; }
}