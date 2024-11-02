﻿using MediatR;


namespace Infrastructure.Contracts.Commands;

public class ConfirmVerificationCodeCommand(
    string verificationCode
) :
    IRequest<bool>
{
    public string VerificationCode { get; private init; } = verificationCode;
}
