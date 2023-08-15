using MediatR;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.UseCases.Skills.Create;

public record CreateSkillQuery(Skill Skill) : IRequest<Skill>;