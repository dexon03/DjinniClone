using MediatR;
using Microsoft.AspNetCore.Mvc;
using VacanciesService.Domain.Contacts;
using VacanciesService.Domain.DTO;
using VacanciesService.Domain.Models;

namespace VacanciesService.Controllers;

public class CategoryController : BaseController
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _categoryService.GetAllCategories();
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _categoryService.GetCategoryById(id);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CategoryCreateDto category)
    {
        var result = await _categoryService.CreateCategory(category);
        return Ok(result);
    }
    
    [HttpPut]
    public async Task<IActionResult> Update(CategoryUpdateDto category)
    {
        var result = await _categoryService.UpdateCategory(category);
        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _categoryService.DeleteCategory(id);
        return Ok();
    }
    
    [HttpDelete("many")]
    public async Task<IActionResult> DeleteMany(Category[] categories)
    {
        await _categoryService.DeleteMany(categories);
        return Ok();
    }
    
}