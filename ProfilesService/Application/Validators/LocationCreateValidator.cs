﻿using FluentValidation;
using ProfilesService.Domain.DTO;

namespace ProfilesService.Application.Validators;

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