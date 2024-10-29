using MediatR;
using Infrastructure.Contracts.Commands;


namespace Infrastructure.Contracts.Interfaces.CommandHandlers;

public interface IRemoveOutboxMessageCommandHandler : IRequestHandler<RemoveOutboxMessageCommand>;
