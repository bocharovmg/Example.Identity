using MediatR;
using FluentValidation;
using Domain.Contracts.Interfaces.Behaviors;

namespace Domain.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IValidationBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
{
    private readonly bool _hasValidators;

    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _hasValidators = validators.Any();

        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_hasValidators)
        {
            return await next();
        }

        var validationContext = new ValidationContext<TRequest>(request);

        var validationResults = await Task
            .WhenAll(
                _validators
                    .Select(v => v.ValidateAsync(validationContext, cancellationToken))
            );

        var validationErrors = validationResults
            .SelectMany(r => r.Errors)
            .Where(f => f != null)
            .ToList();

        if (validationErrors.Any())
        {
            throw new ValidationException(validationErrors);
        }

        return await next();
    }
}
