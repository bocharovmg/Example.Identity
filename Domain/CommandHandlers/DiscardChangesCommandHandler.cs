using Exemple.Identity.Abstractions.Core.Commands;
using Exemple.Identity.Abstractions.Infrastructure.ConnectionManager;
using MediatR;


namespace Exemple.Identity.Abstractions.Core.CommandHandlers;

public class DiscardChangesCommandHandler : IRequestHandler<DiscardChangesCommand>
{
    private readonly ISqlConnectionManager _connectionManager;

    public DiscardChangesCommandHandler(ISqlConnectionManager connectionManager)
    {
        _connectionManager = connectionManager;
    }

    public virtual async Task Handle(DiscardChangesCommand request, CancellationToken cancellationToken)
    {
        await _connectionManager.DiscardChangesAsync(cancellationToken);
    }
}
