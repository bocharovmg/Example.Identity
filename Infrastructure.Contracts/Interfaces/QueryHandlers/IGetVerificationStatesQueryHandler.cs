using MediatR;
using Infrastructure.Contracts.Dtos;
using Infrastructure.Contracts.Queries;


namespace Infrastructure.Contracts.Interfaces.QueryHandlers;

public interface IGetVerificationStatesQueryHandler : IRequestHandler<GetVerificationStatesQuery, IEnumerable<VerificationStateDto>>;
