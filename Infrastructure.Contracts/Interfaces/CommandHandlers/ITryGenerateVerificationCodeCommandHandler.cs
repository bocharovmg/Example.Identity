using MediatR;
using Infrastructure.Contracts.Commands;
using Infrastructure.Contracts.Dtos;


namespace Infrastructure.Contracts.Interfaces.QueryHandlers;

public interface ITryGenerateVerificationCodeCommandHandler : IRequestHandler<TryGenerateVerificationCodeCommand, VerificationCodeDto>;
