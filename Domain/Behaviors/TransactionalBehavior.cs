using Abstractions.Infrastructure.ConnectionManager;
using Domain.Contracts.Interfaces.Behaviors;
using Domain.Contracts.Interfaces.SeedWork;
using MediatR;

namespace Domain.Behaviors;

public class TransactionalBehavior<TRequest, TResponse> : ITransactionalBehavior<TRequest, TResponse> where TRequest : ITransactional
{
    private readonly ISqlConnectionManager _sqlConnectionManager;

    public TransactionalBehavior(ISqlConnectionManager sqlConnectionManager)
    {
        _sqlConnectionManager = sqlConnectionManager;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        await _sqlConnectionManager.BeginChangesAsync(cancellationToken: cancellationToken);

        try
        {
            var response = await next();

            await _sqlConnectionManager.ApplyChangesAsync(cancellationToken: cancellationToken);

            return response;
        }
        catch
        {
            await _sqlConnectionManager.DiscardChangesAsync(cancellationToken: cancellationToken);

            throw;
        }
    }
}
