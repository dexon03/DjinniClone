namespace ChatService.Domain.Dto;

public record ChatDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string SenderOfLastMessage { get; set; }
    public string LastMessage { get; set; }
    public bool IsLastMessageRead { get; set; }
}