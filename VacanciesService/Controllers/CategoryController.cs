using MediatR;
using Microsoft.AspNetCore.Mvc;
using VacanciesService.Application.UseCases.Categories.Create;
using VacanciesService.Application.UseCases.Categories.Delete;
using VacanciesService.Application.UseCases.Categories.DeleteMany;
using VacanciesService.Application.UseCases.Categories.Get;
using VacanciesService.Application.UseCases.Categories.GetAll;
using VacanciesService.Application.UseCases.Categories.Update;
using VacanciesService.Domain.Models;

namespace VacanciesService.Controllers;

public class CategoryController : BaseController
{
    public CategoryController(IMediator mediator) : base(mediator)
    {
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllCategoriesQuery());
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetCategoryQuery(id));
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(Category category)
    {
        var result = await _mediator.Send(new CreateCategoryQuery(category));
        return Ok(result);
    }
    
    [HttpPut]
    public async Task<IActionResult> Update(Category category)
    {
        var result = await _mediator.Send(new UpdateCategoryQuery(category));
        return Ok(result);
    }
    
    [HttpDelete]
    public async Task<IActionResult> Delete(Category category)
    {
        await _mediator.Send(new DeleteCategoryCommand(category));
        return Ok();
    }
    
    [HttpDelete("many")]
    public async Task<IActionResult> DeleteMany(Category[] categories)
    {
        await _mediator.Send(new DeleteManyCategoryCommand(categories));
        return Ok();
    }
    
}