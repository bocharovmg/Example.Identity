using Infrastructure.Contracts.Enums.User;
using Infrastructure.Contracts.Queries;
using MediatR;


namespace Infrastructure.Contracts.Interfaces.QueryHandlers;

public interface IGetVerificationStateTypeQueryHandler : IRequestHandler<GetVerificationStateTypeQuery, VerificationStateType>;
