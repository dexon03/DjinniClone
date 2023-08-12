using MediatR;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.UseCases.Locations.Delete;

public record DeleteLocationCommand(Location Location) : IRequest;