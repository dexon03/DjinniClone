using MediatR;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.UseCases.Locations.GetAll;

public record GetAllLocationsQuery() : IRequest<List<Location>>;