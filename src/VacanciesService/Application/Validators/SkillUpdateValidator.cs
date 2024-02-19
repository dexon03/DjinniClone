using FluentValidation;
using VacanciesService.Domain.DTO;

namespace VacanciesService.Application.Validators;

public class SkillUpdateValidator : AbstractValidator<SkillUpdateDto>
{
    public SkillUpdateValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required");
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required");
    }
}