using MediatR;
using Infrastructure.Contracts.Queries;


namespace Infrastructure.Contracts.Interfaces.QueryHandlers;

public interface IGetProcessedOutboxMessagesQueryHandler : IRequestHandler<GetProcessedOutboxMessagesQuery, IEnumerable<Guid>>;
