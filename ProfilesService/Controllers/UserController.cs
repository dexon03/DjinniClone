using Microsoft.AspNetCore.Mvc;
using ProfilesService.Domain.Contracts;
using ProfilesService.Domain.DTO;

namespace ProfilesService.Controllers;

public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _userService.GetAllUsers();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _userService.GetUserById(id);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(UserCreateDto user)
    {
        var result = await _userService.CreateUser(user);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UserUpdateDto user)
    {
        var result = await _userService.UpdateUser(user);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _userService.DeleteUser(id);
        return Ok();
    }
}