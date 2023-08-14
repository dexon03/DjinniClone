using MediatR;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.UseCases.Categories.Create;

public record CreateCategoryQuery(Category category) : IRequest<Category>;