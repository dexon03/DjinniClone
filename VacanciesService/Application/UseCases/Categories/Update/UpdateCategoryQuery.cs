using MediatR;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.UseCases.Categories.Update;

public record UpdateCategoryQuery(Category category) : IRequest<Category>;