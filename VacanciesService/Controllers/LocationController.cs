using MediatR;
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
        var result = await _locationService.GetAllLocations();
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _locationService.GetLocationById(id);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(LocationCreateDto location)
    {
        var result = await _locationService.CreateLocation(location);
        return Ok(result);
    }
    
    [HttpPut]
    public async Task<IActionResult> Update(LocationUpdateDto location)
    {
        var result = await _locationService.UpdateLocation(location);
        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _locationService.DeleteLocation(id);
        return Ok();
    }
    
    [HttpDelete("many")]
    public async Task<IActionResult> DeleteMany(Location[] locations)
    {
        await _locationService.DeleteManyLocations(locations);
        return Ok();
    }
}