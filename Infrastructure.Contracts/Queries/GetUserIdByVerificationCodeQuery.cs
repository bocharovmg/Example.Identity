﻿using MediatR;


namespace Infrastructure.Contracts.Queries;

public class GetUserIdByVerificationCodeQuery(
    string verificationCode
) :
    IRequest<Guid>
{
    public string VerificationCode { get; private init; } = verificationCode;
}
