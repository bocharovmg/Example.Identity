﻿using Infrastructure.Contracts.Commands;
using MediatR;


namespace Infrastructure.Contracts.Interfaces.QueryHandlers;

public interface IConfirmVerificationCodeCommandHandler : IRequestHandler<ConfirmVerificationCodeCommand, bool>;
