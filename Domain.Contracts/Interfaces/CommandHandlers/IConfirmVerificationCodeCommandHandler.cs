using Domain.Contracts.Commands;
using MediatR;


namespace Domain.Contracts.Interfaces.CommandHandlers;

public interface IConfirmVerificationCodeCommandHandler : IRequestHandler<ConfirmVerificationCodeCommand, bool>;
