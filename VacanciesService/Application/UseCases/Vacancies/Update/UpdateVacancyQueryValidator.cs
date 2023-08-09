using FluentValidation;

namespace VacanciesService.Application.UseCases.Vacancies.Update;

public class UpdateVacancyQueryValidator : AbstractValidator<UpdateVacancyQuery>
{
    public UpdateVacancyQueryValidator()
    {
        RuleFor(_ => _.vacancy.Title).NotEmpty().MinimumLength(3);
        RuleFor(_ => _.vacancy.Description).NotEmpty().MinimumLength(20);
        RuleFor(_ => _.vacancy.Salary).NotEmpty().GreaterThanOrEqualTo(0);
        // RuleFor(_ => _.vacancy.Category).NotEmpty();
        // RuleFor(_ => _.vacancy.Company).NotEmpty();
        RuleFor(_ => _.vacancy.PositionTitle).NotEmpty().MinimumLength(3);
        RuleFor(_ => _.vacancy.LocationVacancies).NotEmpty();
        RuleFor(_ => _.vacancy.VacancySkills).NotEmpty();
        RuleFor(_ => _.vacancy.CategoryId).NotEmpty();
        RuleFor(_ => _.vacancy.CompanyId).NotEmpty();
        RuleFor(_ => _.vacancy.IsActive).NotEmpty();
    }
}