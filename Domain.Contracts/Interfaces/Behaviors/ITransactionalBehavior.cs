using MediatR;
using Domain.Contracts.Interfaces.SeedWork;


namespace Domain.Contracts.Interfaces.Behaviors;

public interface ITransactionalBehavior<in TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ITransactional;
