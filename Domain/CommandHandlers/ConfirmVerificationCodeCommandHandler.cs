using MediatR;
using AutoMapper;
using DomainCommands = Domain.Contracts.Commands;
using Domain.Contracts.Interfaces.CommandHandlers;
using InfrastructureCommands = Infrastructure.Contracts.Commands;


namespace Domain.CommandHandlers;

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
