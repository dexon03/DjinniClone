using Core.Database;
using MediatR;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.UseCases.Categories.Get;

public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, Category>
{
    private readonly IRepository _repository;

    public GetCategoryQueryHandler(IRepository repository)
    {
        _repository = repository;
    }
    public async Task<Category> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var category = await _repository.GetByIdAsync<Category>(request.id);
        if (category == null)
        {
            throw new Exception("Category not found");
        }
        
        return category;
    }
}