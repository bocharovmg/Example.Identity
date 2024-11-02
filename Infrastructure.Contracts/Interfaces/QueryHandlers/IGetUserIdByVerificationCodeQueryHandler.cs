using Infrastructure.Contracts.Queries;
using MediatR;


namespace Infrastructure.Contracts.Interfaces.QueryHandlers;

public interface IGetUserIdByVerificationCodeQueryHandler : IRequestHandler<GetUserIdByVerificationCodeQuery, Guid>;
