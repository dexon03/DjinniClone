using MediatR;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.UseCases.Locations.Create;

public record CreateLocationQuery(Location location) : IRequest<Location>;