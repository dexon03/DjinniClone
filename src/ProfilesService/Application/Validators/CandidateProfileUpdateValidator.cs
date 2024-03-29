﻿using Core.Database;
using FluentValidation;
using ProfilesService.Domain.DTO;

namespace ProfilesService.Application.Validators;

public class CandidateProfileUpdateValidator : AbstractValidator<CandidateProfileUpdateDto>
{
    public CandidateProfileUpdateValidator(IRepository repository)
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Surname).NotEmpty().WithMessage("Surname is required");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email is required");
    }
}
