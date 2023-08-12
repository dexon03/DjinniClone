using MediatR;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.UseCases.Skills.DeleteMany;

public record DeleteManyCommand(Skill[] Skills) : IRequest;