using Microsoft.AspNetCore.Authorization;
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
        return Ok(await _categoryService.GetAllCategories());
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(await _categoryService.GetCategoryById(id));
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(CategoryCreateDto category)
    {
        return Ok(await _categoryService.CreateCategory(category));
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<IActionResult> Update(CategoryUpdateDto category)
    {
        return Ok(await _categoryService.UpdateCategory(category));
    }
    
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _categoryService.DeleteCategory(id);
        return Ok();
    }
    
    [Authorize(Roles = "Admin")]
    [HttpDelete("many")]
    public async Task<IActionResult> DeleteMany(Category[] categories)
    {
        await _categoryService.DeleteMany(categories);
        return Ok();
    }
    
}