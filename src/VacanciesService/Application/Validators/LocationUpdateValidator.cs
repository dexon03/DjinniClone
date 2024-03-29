﻿using FluentValidation;
using VacanciesService.Domain.DTO;

namespace VacanciesService.Application.Validators;

public class LocationUpdateValidator : AbstractValidator<LocationUpdateDto>
{
    public LocationUpdateValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required");
        RuleFor(x => x.City)
            .NotEmpty()
            .WithMessage("City is required");
        RuleFor(x => x.Country)
            .NotEmpty()
            .WithMessage("Country is required");
    }
}