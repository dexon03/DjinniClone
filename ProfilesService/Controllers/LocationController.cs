using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfilesService.Domain.Contracts;
using ProfilesService.Domain.DTO;
using ProfilesService.Domain.Models;

namespace ProfilesService.Controllers;

[Authorize]
public class LocationController : BaseController
{
    private readonly ILocationService _locationService;

    public LocationController(ILocationService locationService)
    {
        _locationService = locationService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _locationService.GetAllLocations();
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _locationService.GetLocationById(id);
        return Ok(result);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(LocationCreateDto location)
    {
        var result = await _locationService.CreateLocation(location);
        return Ok(result);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<IActionResult> Update(LocationUpdateDto location)
    {
        var result = await _locationService.UpdateLocation(location);
        return Ok(result);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _locationService.DeleteLocation(id);
        return Ok();
    }
    
    [Authorize(Roles = "Admin")]
    [HttpDelete("many")]
    public async Task<IActionResult> DeleteMany(Location[] locations)
    {
        await _locationService.DeleteManyLocations(locations);
        return Ok();
    }
}