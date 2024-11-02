using Domain.Contracts.Commands;
using MediatR;


namespace Domain.Contracts.Interfaces.CommandHandlers;

public interface ISetupPasswordCommandCommandHandler : IRequestHandler<SetupPasswordCommand, bool>;
