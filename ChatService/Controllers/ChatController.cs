using ChatService.Domain.Contracts;
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
    public async Task<IActionResult> GetChats(Guid userId)
    {
        var chats = await _chatService.GetChatList(userId);
        return Ok(chats);
    }
}