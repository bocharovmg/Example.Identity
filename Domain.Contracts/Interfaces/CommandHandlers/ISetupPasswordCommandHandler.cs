using Domain.Contracts.Commands;
using MediatR;


namespace Domain.Contracts.Interfaces.CommandHandlers;

public interface ISetupPasswordCommandHandler : IRequestHandler<SetupPasswordCommand>;
