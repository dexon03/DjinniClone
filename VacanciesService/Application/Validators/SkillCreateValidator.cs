using FluentValidation;
using VacanciesService.Domain.DTO;

namespace VacanciesService.Application.Validators;

public class SkillCreateValidator : AbstractValidator<SkillCreateDto>
{
    public SkillCreateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required");
    }
    
}