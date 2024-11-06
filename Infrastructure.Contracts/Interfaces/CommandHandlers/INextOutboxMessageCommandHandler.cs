using MediatR;
using Infrastructure.Contracts.Dtos;
using Infrastructure.Contracts.Commands;


namespace Infrastructure.Contracts.Interfaces.CommandHandlers;

public interface INextOutboxMessageCommandHandler : IRequestHandler<NextOutboxMessageCommand, OutboxMessageDto?>;
