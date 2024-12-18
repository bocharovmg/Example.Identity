﻿using MediatR;


namespace Domain.Contracts.Commands;

public class SetupPasswordCommand(
    string email,
    string verificationCode,
    string password
) :
    IRequest
{
    public string Email { get; private init; } = email;

    public string VerificationCode { get; private init; } = verificationCode;

    public string Password { get; private init; } = password;
}
