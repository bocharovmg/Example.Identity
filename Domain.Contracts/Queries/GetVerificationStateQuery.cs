﻿using Exemple.Identity.Domain.Contracts.Dtos;
using MediatR;


namespace Exemple.Identity.Domain.Contracts.Queries;

public class GetVerificationStateQuery(
    string email
) :
    IRequest<VerificationStateDto>
{
    public virtual string Email { get; private init; } = email;
}
