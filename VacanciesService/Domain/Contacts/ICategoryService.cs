using VacanciesService.Domain.DTO;
using VacanciesService.Domain.Models;

namespace VacanciesService.Domain.Contacts;

public interface ICategoryService
{
    Task<List<Category>> GetAllCategories(CancellationToken cancellationToken = default);
    Task<Category> GetCategoryById(Guid id, CancellationToken cancellationToken = default);
    Task<Category> CreateCategory(CategoryCreateDto category, CancellationToken cancellationToken = default);
    Task<Category> UpdateCategory(CategoryUpdateDto category, CancellationToken cancellationToken = default);
    Task DeleteCategory(Guid id, CancellationToken cancellationToken = default);
    Task DeleteMany (Category[] categories, CancellationToken cancellationToken = default);
}