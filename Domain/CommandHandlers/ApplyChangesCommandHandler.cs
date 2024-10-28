using Exemple.Identity.Abstractions.Core.Commands;
using Exemple.Identity.Abstractions.Infrastructure.ConnectionManager;
using MediatR;


namespace Exemple.Identity.Abstractions.Core.CommandHandlers;

public class ApplyChangesCommandHandler : IRequestHandler<ApplyChangesCommand>
{
    private readonly ISqlConnectionManager _connectionManager;

    public ApplyChangesCommandHandler(ISqlConnectionManager connectionManager)
    {
        _connectionManager = connectionManager;
    }

    public virtual async Task Handle(ApplyChangesCommand request, CancellationToken cancellationToken)
    {
        await _connectionManager.ApplyChangesAsync(cancellationToken);
    }
}
