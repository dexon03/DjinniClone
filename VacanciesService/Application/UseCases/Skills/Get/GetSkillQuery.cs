using MediatR;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.UseCases.Skills.Get;

public record GetSkillQuery(Guid id) :IRequest<Skill>;