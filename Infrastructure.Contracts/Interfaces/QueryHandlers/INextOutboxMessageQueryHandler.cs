using MediatR;
using Infrastructure.Contracts.Queries;
using Infrastructure.Contracts.Dtos;


namespace Infrastructure.Contracts.Interfaces.QueryHandlers;

public interface INextOutboxMessageQueryHandler : IRequestHandler<NextOutboxMessageQuery, OutboxMessageDto?>;
