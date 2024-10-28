using Exemple.Identity.Domain.Contracts.Commands;
using MediatR;


namespace Exemple.Identity.Domain.Contracts.Interfaces.CommandHandlers;

public interface ISetupPasswordCommandCommandHandler : IRequestHandler<SetupPasswordCommand, bool>;
