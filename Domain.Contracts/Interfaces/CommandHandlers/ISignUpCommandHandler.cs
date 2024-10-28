﻿using Exemple.Identity.Domain.Contracts.Commands;
using Exemple.Identity.Domain.Contracts.Dtos;
using MediatR;


namespace Exemple.Identity.Domain.Contracts.Interfaces.CommandHandlers;

public interface ISignUpCommandHandler : IRequestHandler<SignUpCommand, AuthDto>;
