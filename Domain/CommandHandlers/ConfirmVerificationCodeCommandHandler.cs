using MediatR;
using AutoMapper;
using DomainCommands = Exemple.Identity.Domain.Contracts.Commands;
using Exemple.Identity.Domain.Contracts.Interfaces.CommandHandlers;
using InfrastructureCommands = Exemple.Identity.Infrastructure.Contracts.Commands;


namespace Exemple.Identity.Domain.CommandHandlers;

public class ConfirmVerificationCodeCommandHandler : IConfirmVerificationCodeCommandHandler
{
    private readonly IMediator _mediator;

    private readonly IMapper _mapper;

    public ConfirmVerificationCodeCommandHandler(
        IMediator mediator,
        IMapper mapper
    )
    {
        _mediator = mediator;

        _mapper = mapper;
    }

    public async Task<bool> Handle(DomainCommands.ConfirmVerificationCodeCommand request, CancellationToken cancellationToken)
    {
        var confirmVerificationCodeRequest = _mapper.Map<InfrastructureCommands.ConfirmVerificationCodeCommand>(request);

        return await _mediator.Send(confirmVerificationCodeRequest, cancellationToken);
    }
}
