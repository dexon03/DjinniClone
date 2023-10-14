using Core.Database;
using IdentityService.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class UserController : ControllerBase
{
    private readonly UserManager _userManager;

    public UserController(UserManager userManager)
    {
        _userManager = userManager;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userManager.GetUsers();
        return Ok(users);
    }
}