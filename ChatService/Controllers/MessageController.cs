using ChatService.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessageController : ControllerBase
{
    public MessageController()
    {
        ; }
}