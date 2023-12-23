using Microsoft.AspNetCore.Mvc;

namespace ChatService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    public ChatController()
    {
        
    }
    
    [HttpGet("chats/{userId}")]
    public async Task<IActionResult> GetChats(Guid userId)
    {
        return Ok();
    }
}