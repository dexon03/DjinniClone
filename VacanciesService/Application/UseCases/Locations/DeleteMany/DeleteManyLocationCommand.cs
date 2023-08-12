using MediatR;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.UseCases.Locations.DeleteMany;

public record DeleteManyLocationCommand(Location[] Locations) : IRequest;