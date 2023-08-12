using FluentValidation;

namespace VacanciesService.Application.UseCases.Locations.Create;

public class CreateLocationValidator : AbstractValidator<CreateLocationQuery>
{
    public CreateLocationValidator()
    {
        RuleFor(x => x.location.City).MinimumLength(2);
        RuleFor(x => x.location.Country).MinimumLength(2);
    }
}