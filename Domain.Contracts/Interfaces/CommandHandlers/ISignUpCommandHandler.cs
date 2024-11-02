using Domain.Contracts.Interfaces.SeedWork;
using Domain.Contracts.Commands;
using Domain.Contracts.Dtos;
using MediatR;


namespace Domain.Contracts.Interfaces.CommandHandlers;

public interface ISignUpCommandHandler : IRequestHandler<SignUpCommand, AuthDto>;
