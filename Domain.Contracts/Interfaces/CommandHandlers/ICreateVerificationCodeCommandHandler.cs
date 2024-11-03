using Domain.Contracts.Commands;
using Domain.Contracts.Dtos;
using MediatR;


namespace Domain.Contracts.Interfaces.CommandHandlers;

public interface ICreateVerificationCodeCommandHandler : IRequestHandler<CreateVerificationCodeCommand, VerificationStateDto>;
