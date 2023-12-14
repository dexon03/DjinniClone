using System.Net;
using Core.Database;
using Core.Exceptions;
using FluentValidation;
using VacanciesService.Domain.DTO;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.Validators;

public class VacancyUpdateValidator : AbstractValidator<VacancyUpdateDto>
{
    public VacancyUpdateValidator(IRepository repository)
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
        RuleFor(x => x.PositionTitle).NotEmpty().WithMessage("PositionTitle is required");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        RuleFor(x => x.Salary).NotEmpty().GreaterThan(0).WithMessage("Salary is required");
        RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Category is required").Custom((id, context) =>
        {
            if (!repository.Any<Category>(c => c.Id == id))
            {
                context.AddFailure("Category not found");
            }
        });
    }
}