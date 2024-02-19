using FluentValidation;
using VacanciesService.Domain.DTO;

namespace VacanciesService.Application.Validators;

public class LocationCreateValidator : AbstractValidator<LocationCreateDto>
{
    public LocationCreateValidator()
    {
        RuleFor(x => x.City)
            .NotEmpty()
            .WithMessage("City is required");
        RuleFor(x => x.Country)
            .NotEmpty()
            .WithMessage("Country is required");
    }
}