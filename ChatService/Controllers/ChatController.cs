using ChatService.Domain.Contracts;
using ChatService.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ChatService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;

    public ChatController(IChatService chatService)
    {
        _chatService = chatService;
    }
    
    [HttpGet("list/{userId}")]
    public async Task<IActionResult> GetChats(Guid userId, CancellationToken cancellationToken)
    {
        var chats = await _chatService.GetChatList(userId,cancellationToken);
        return Ok(chats);
    }
    
    [HttpGet("messages/{chatId}")]
    public async Task<IActionResult> GetChatMessages(Guid chatId, CancellationToken cancellationToken)
    {
        var messages = await _chatService.GetChatMessages(chatId, cancellationToken);
        return Ok(messages);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateChat(CreateChatDto chatDto, CancellationToken cancellationToken)
    {
        await _chatService.CreateChat(chatDto, cancellationToken);
        return Ok();
    }
    
}