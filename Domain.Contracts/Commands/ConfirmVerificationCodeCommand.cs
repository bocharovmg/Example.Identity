﻿using MediatR;


namespace Exemple.Identity.Domain.Contracts.Commands;

public class ConfirmVerificationCodeCommand : IRequest<bool>
{
    public virtual string VerificationCode { get; private init; }

    public ConfirmVerificationCodeCommand(string verificationCode)
    {
        VerificationCode = verificationCode;
    }
}
