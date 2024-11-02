﻿using Infrastructure.Contracts.Commands;
using MediatR;


namespace Infrastructure.Contracts.Interfaces.QueryHandlers;

public interface IBlockUserAccessCommandHandler : IRequestHandler<BlockUserAccessCommand, bool>;
