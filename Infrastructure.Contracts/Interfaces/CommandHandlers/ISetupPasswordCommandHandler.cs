using Infrastructure.Contracts.Commands;
using MediatR;


namespace Infrastructure.Contracts.Interfaces.QueryHandlers;

public interface ISetupPasswordCommandHandler : IRequestHandler<SetupPasswordCommand, bool>;
