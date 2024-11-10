using MediatR;


namespace Domain.Contracts.Interfaces.Behaviors;

public interface IValidationBehavior<in TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>;