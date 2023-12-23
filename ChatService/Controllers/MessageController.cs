using ChatService.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessageController : ControllerBase
{
    private readonly IHubContext<ChatHub> _hubContext;

    public MessageController(IHubContext<ChatHub> hubContext)
    {
        _hubContext = hubContext;
    }
}