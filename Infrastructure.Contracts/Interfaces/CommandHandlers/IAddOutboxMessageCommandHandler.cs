using MediatR;
using Infrastructure.Contracts.Commands;


namespace Infrastructure.Contracts.Interfaces.CommandHandlers;

public interface IAddOutboxMessageCommandHandler : IRequestHandler<AddOutboxMessageCommand>;
