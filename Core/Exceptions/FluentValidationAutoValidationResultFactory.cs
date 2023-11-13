using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;

namespace Core.Exceptions;

public class FluentValidationAutoValidationResultFactory : IFluentValidationAutoValidationResultFactory
{
    public IActionResult CreateActionResult(ActionExecutingContext context, ValidationProblemDetails? validationProblemDetails)
    {
        // if (validationProblemDetails is null)
        // {
        //     return new BadRequestResult();
        // }
        //
        // var errorJson = JsonConvert.SerializeObject(validationProblemDetails.Errors);
        //
        // return new BadRequestObjectResult(errorJson);
        throw new NotImplementedException();
    }
}