using Exemple.Identity.Domain.Contracts.Commands;
using MediatR;


namespace Exemple.Identity.Domain.Contracts.Interfaces.CommandHandlers;

public interface IConfirmVerificationCodeCommandHandler : IRequestHandler<ConfirmVerificationCodeCommand, bool>;
