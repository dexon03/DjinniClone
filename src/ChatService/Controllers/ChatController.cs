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
    public async Task<IActionResult> GetChatList(Guid userId, CancellationToken cancellationToken)
    {
        return Ok(await _chatService.GetChatList(userId,cancellationToken));
    }
    
    [HttpGet("messages/{chatId}")]
    public async Task<IActionResult> GetChatMessages(Guid chatId, CancellationToken cancellationToken)
    {
        return Ok(await _chatService.GetChatMessages(chatId, cancellationToken));
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateChat(CreateChatDto chatDto, CancellationToken cancellationToken)
    {
        await _chatService.CreateChat(chatDto, cancellationToken);
        return Ok();
    }
    
}