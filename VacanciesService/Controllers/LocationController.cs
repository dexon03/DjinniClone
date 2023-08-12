using MediatR;
using Microsoft.AspNetCore.Mvc;
using VacanciesService.Application.UseCases.Locations.Create;
using VacanciesService.Application.UseCases.Locations.Delete;
using VacanciesService.Application.UseCases.Locations.DeleteMany;
using VacanciesService.Application.UseCases.Locations.Get;
using VacanciesService.Application.UseCases.Locations.GetAll;
using VacanciesService.Application.UseCases.Locations.Update;
using VacanciesService.Domain.Models;

namespace VacanciesService.Controllers;

public class LocationController : BaseController
{
    public LocationController(IMediator mediator) : base(mediator)
    {
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllLocationsQuery());
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetLocationQuery(id));
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(Location location)
    {
        var result = await _mediator.Send(new CreateLocationQuery(location));
        return Ok(result);
    }
    
    [HttpPut]
    public async Task<IActionResult> Update(Location location)
    {
        var result = await _mediator.Send(new UpdateLocationQuery(location));
        return Ok(result);
    }
    
    [HttpDelete]
    public async Task<IActionResult> Delete( Location location)
    {
        await _mediator.Send(new DeleteLocationCommand(location));
        return Ok();
    }
    
    [HttpDelete("many")]
    public async Task<IActionResult> DeleteMany([FromBody] Location[] locations)
    {
        await _mediator.Send(new DeleteManyLocationCommand(locations));
        return Ok();
    }
}