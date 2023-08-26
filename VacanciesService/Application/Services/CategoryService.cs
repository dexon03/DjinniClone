using AutoMapper;
using Core.Database;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using VacanciesService.Domain.Contacts;
using VacanciesService.Domain.DTO;
using VacanciesService.Domain.Models;
using ValidationException = Core.Exceptions.ValidationException;

namespace VacanciesService.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly IRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<CategoryCreateDto> _createValidator;
    private readonly IValidator<CategoryUpdateDto> _updateValidator;

    public CategoryService(IRepository repository, IMapper mapper, IValidator<CategoryCreateDto> createValidator, IValidator<CategoryUpdateDto> updateValidator)
    {
        _repository = repository;
        _mapper = mapper;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }
    public Task<List<Category>> GetAllCategories(CancellationToken cancellationToken = default)
    {
        return _repository.GetAll<Category>().ToListAsync(cancellationToken);
    }

    public async Task<Category> GetCategoryById(Guid id, CancellationToken cancellationToken = default)
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
        var validationResult = await _createValidator.ValidateAsync(category,cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var categoryEntity = _mapper.Map<Category>(category);
        var result =  await _repository.CreateAsync(categoryEntity);
        await _repository.SaveChangesAsync(cancellationToken);
        return result;
    }

    public async Task<Category> UpdateCategory(CategoryUpdateDto category, CancellationToken cancellationToken = default)
    {
        var validationResult = await _updateValidator.ValidateAsync(category,cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
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