using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;

namespace Core.Validation;

public class FluentValidationAutoValidationCustomResultFactory : IFluentValidationAutoValidationResultFactory
{
    public IActionResult CreateActionResult(ActionExecutingContext context, ValidationProblemDetails? validationProblemDetails)
    {
        return new UnprocessableEntityObjectResult(validationProblemDetails.Errors);
    }
}