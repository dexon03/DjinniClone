using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VacanciesService.Domain.Contacts;
using VacanciesService.Domain.DTO;
using VacanciesService.Domain.Models;

namespace VacanciesService.Controllers;

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
        return Ok(await _locationService.GetAllLocations());
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(await _locationService.GetLocationById(id));
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(LocationCreateDto location)
    {
        return Ok(await _locationService.CreateLocation(location));
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<IActionResult> Update(LocationUpdateDto location)
    {
        return Ok(await _locationService.UpdateLocation(location));
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