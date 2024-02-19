using FluentValidation;
using VacanciesService.Domain.DTO;

namespace VacanciesService.Application.Validators;

public class CompanyUpdateValidator : AbstractValidator<CompanyUpdateDto>
{
    public CompanyUpdateValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
    }
}