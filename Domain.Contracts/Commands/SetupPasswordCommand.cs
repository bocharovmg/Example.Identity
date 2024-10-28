using MediatR;


namespace Exemple.Identity.Domain.Contracts.Commands;

public class SetupPasswordCommand : IRequest<bool>
{
    public virtual string VerificationCode { get; private init; }

    public virtual string Password { get; private init; }

    public SetupPasswordCommand(
        string verificationCode,
        string password
    )
    {
        VerificationCode = verificationCode;

        Password = password;
    }
}
