

using IdentityService.Domain.Contracts;
using IdentityService.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IJWTService _jwtService;

    public AuthController(IJWTService jwtService)
    {
        _jwtService = jwtService;
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login()
    {
        return Ok();
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register()
    {
        return Ok();
    }
    
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh()
    {
        return Ok();
    }
    
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        return Ok();
    }
    
    [HttpGet("healthCheck")]
    public IActionResult HealthCheck()
    {
        return Ok();
    }
    
    [HttpPost("forgotPassword")]
    public async Task<IActionResult> ForgotPassword()
    {
        return Ok();
    }
}