using AutoMapper;
using MediatR;
using DomainCommands = Exemple.Identity.Domain.Contracts.Commands;
using Exemple.Identity.Domain.Contracts.Dtos;
using Exemple.Identity.Domain.Contracts.Interfaces.CommandHandlers;
using InfrastructureCommands = Exemple.Identity.Infrastructure.Contracts.Commands;
using Exemple.Identity.Abstractions.Core.Commands;
using DomainQueries = Exemple.Identity.Domain.Contracts.Queries;
using Exemple.Identity.Domain.Contracts.Enums.User;


namespace Exemple.Identity.Domain.CommandHandlers;

public class SignUpCommandHandler : ISignUpCommandHandler
{
    private readonly IMediator _mediator;

    private readonly IMapper _mapper;

    public SignUpCommandHandler(
        IMediator mediator,
        IMapper mapper
    )
    {
        _mediator = mediator;

        _mapper = mapper;
    }

    public async Task<AuthDto> Handle(DomainCommands.SignUpCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(new BeginChangesCommand(), cancellationToken);

        try
        {
            #region create user
            var createUserRequest = _mapper.Map<InfrastructureCommands.CreateUserCommand>(request);

            var userId = await _mediator.Send(createUserRequest, cancellationToken);
            #endregion

            #region create verification code
            var createVerificationCodeRequest = new DomainCommands.CreateAndSendVerificationCodeCommand(request.Email, VerificationFieldType.Email);

            await _mediator.Send(createVerificationCodeRequest, cancellationToken);
            #endregion

            await _mediator.Send(new ApplyChangesCommand(), cancellationToken);

            #region get user
            var getUserRequest = new DomainQueries.GetUserQuery(userId);

            var user = await _mediator.Send(getUserRequest, cancellationToken);
            #endregion

            #region get security token
            var getSecurityTokenRequest = _mapper.Map<DomainQueries.GetSecurityTokenQuery>(user);

            var securityToken = await _mediator.Send(getSecurityTokenRequest, cancellationToken);
            #endregion

            return new AuthDto
            {
                User = user,
                SecurityToken = securityToken
            };
        }
        catch
        {
            await _mediator.Send(new DiscardChangesCommand(), cancellationToken);

            throw;
        }
    }
}
