using MediatR;


namespace Infrastructure.Contracts.Queries;

public class GetProcessedOutboxMessagesQuery : IRequest<IEnumerable<Guid>>;
