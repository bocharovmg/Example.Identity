using Exemple.Identity.Infrastructure.Contracts.Commands;
using MediatR;


namespace Exemple.Identity.Infrastructure.Contracts.Interfaces.QueryHandlers;

public interface IBlockUserAccessCommandHandler : IRequestHandler<BlockUserAccessCommand, bool>;
