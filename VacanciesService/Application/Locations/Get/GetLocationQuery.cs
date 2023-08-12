using MediatR;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.UseCases.Locations.Get;

public record GetLocationQuery(Guid Id) : IRequest<Location>;