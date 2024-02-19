using AutoMapper;
using Core.Database;
using Microsoft.EntityFrameworkCore;
using VacanciesService.Domain.Contacts;
using VacanciesService.Domain.DTO;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly IRepository _repository;
    private readonly IMapper _mapper;

    public CategoryService(IRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public Task<List<Category>> GetAllCategories(CancellationToken cancellationToken = default)
    {
        return _repository.GetAll<Category>().ToListAsync(cancellationToken);
    }

    public async Task<Category> GetCategoryById(Guid id)
    {
        var category = await _repository.GetByIdAsync<Category>(id);
        if (category == null)
        {
            throw new Exception("Category not found");
        }
        
        return category;
    }

    public async Task<Category> CreateCategory(CategoryCreateDto category, CancellationToken cancellationToken = default)
    {
        var categoryEntity = _mapper.Map<Category>(category);
        var result =  await _repository.CreateAsync(categoryEntity);
        await _repository.SaveChangesAsync(cancellationToken);
        return result;
    }

    public async Task<Category> UpdateCategory(CategoryUpdateDto category, CancellationToken cancellationToken = default)
    {
        var categoryEntity = _mapper.Map<Category>(category);
        var isExists = await _repository.AnyAsync<Category>(x => x.Id == categoryEntity.Id);
        if (!isExists)
        {
            throw new Exception("Category not found");
        }

        var result = _repository.Update(categoryEntity);
        await _repository.SaveChangesAsync(cancellationToken);
        return result;
    }

    public async Task DeleteCategory(Guid id, CancellationToken cancellationToken = default)
    {
        var category = await _repository.GetByIdAsync<Category>(id);
        if (category == null)
        {
            throw new Exception("Category not found");
        }
        
        _repository.Delete(category);
        await _repository.SaveChangesAsync(cancellationToken);
    }
    
    public async Task DeleteMany(Category[] categories, CancellationToken cancellationToken = default)
    {
        _repository.DeleteRange(categories);
        await _repository.SaveChangesAsync(cancellationToken);
    }
}