using Core.Exceptions;
using FluentValidation;
using MediatR;
using ValidationException = FluentValidation.ValidationException;

namespace VacanciesService.Application.Behaviors;

public sealed class ValidationPipelineBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators; 
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any()) return await next();

        var context = new ValidationContext<TRequest>(request);
        var validateResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
        var failures = validateResults
            .Where(validationResult => !validationResult.IsValid)
            .SelectMany(result => result.Errors)
            .Select(validationFailure => new ValidationError
            (
                validationFailure.PropertyName,
                validationFailure.ErrorMessage
            ))
            .ToList();

        if (failures.Any())
        {
            throw new Core.Exceptions.ValidationException(failures);
        }

        return await next();
    }
}