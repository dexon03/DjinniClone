using MediatR;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.UseCases.Categories.GetAll;

public record GetAllCategoriesQuery() : IRequest<List<Category>>;