using Exemple.Identity.Abstractions.Core.Commands;
using Exemple.Identity.Abstractions.Infrastructure.ConnectionManager;
using MediatR;


namespace Exemple.Identity.Abstractions.Core.CommandHandlers;

public class BeginChangesCommandHandler : IRequestHandler<BeginChangesCommand>
{
    private readonly ISqlConnectionManager _connectionManager;

    public BeginChangesCommandHandler(ISqlConnectionManager connectionManager)
    {
        _connectionManager = connectionManager;
    }

    public virtual async Task Handle(BeginChangesCommand request, CancellationToken cancellationToken)
    {
        await _connectionManager.BeginChangesAsync(System.Data.IsolationLevel.ReadCommitted, cancellationToken);
    }
}
