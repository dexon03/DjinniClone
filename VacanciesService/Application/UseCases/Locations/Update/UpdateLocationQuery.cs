using MediatR;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.UseCases.Locations.Update;

public record UpdateLocationQuery(Location location) : IRequest<Location>;