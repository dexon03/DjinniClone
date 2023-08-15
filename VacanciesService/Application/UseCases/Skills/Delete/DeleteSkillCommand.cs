using MediatR;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.UseCases.Skills.Delete;

public record DeleteSkillCommand(Skill Skill) : IRequest;