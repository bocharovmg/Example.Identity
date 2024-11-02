using Domain.Contracts.Commands;
using Domain.Contracts.Dtos;
using MediatR;


namespace Domain.Contracts.Interfaces.CommandHandlers;

public interface ICreateAndSendVerificationCodeCommandHandler : IRequestHandler<CreateAndSendVerificationCodeCommand, VerificationStateDto>;
