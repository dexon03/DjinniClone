using Core.Database;
using IdentityService.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class RoleController : ControllerBase
{
    private readonly IRepository _repository;

    public RoleController(IRepository repository)
    {
        _repository = repository;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetRoles()
    {
        var roles = await _repository
            .GetAll<Role>()
            .ToListAsync();
        return Ok(roles);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRole(Guid id)
    {
        var role = await _repository.GetByIdAsync<Role>(id);
        return Ok(role);
    }
}