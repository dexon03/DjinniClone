using MediatR;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.UseCases.Categories.DeleteMany;

public record DeleteManyCategoryCommand(Category[] categories) : IRequest;