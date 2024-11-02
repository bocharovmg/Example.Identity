using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Api.Contracts.Requests.VerificationCode;
using Infrastructure.Contracts;
using DomainCommands = Domain.Contracts.Commands;
using DomainQueries = Domain.Contracts.Queries;


namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class NotificationController : ControllerBase
{
    private ILogger<NotificationController> _logger;

    private readonly IMediator _mediator;

    private readonly UserContext _userContext;

    public NotificationController(
        ILogger<NotificationController> logger,
        IMediator mediator,
        UserContext userContext
    )
    {
        _logger = logger;

        _mediator = mediator;

        _userContext = userContext;
    }

    [HttpPost("verification-code/restore-password")]
    [AllowAnonymous]
    public async Task<IActionResult> RestorePasswordCodeRequestAsync(
        [FromBody] RestorePasswordVerificationCodeRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var createVerificationCodeRequest = new DomainCommands
            .CreateAndSendVerificationCodeCommand(
                request.Email,
                Domain.Contracts.Enums.User.VerificationFieldType.Password
            );

        var verificationState = await _mediator.Send(createVerificationCodeRequest, cancellationToken);

        return Ok(verificationState);
    }

    [HttpPost("verification-code/resend-email")]
    public async Task<IActionResult> ResendEmailCodeRequestAsync(
        CancellationToken cancellationToken = default
    )
    {
        #region get user
        var getUserRequest = new DomainQueries.GetUserQuery(_userContext.UserId);

        var user = await _mediator.Send(getUserRequest, cancellationToken);
        #endregion

        var createVerificationCodeRequest = new DomainCommands
            .CreateAndSendVerificationCodeCommand(
                user.Email,
                Domain.Contracts.Enums.User.VerificationFieldType.Email
            );

        var verificationState = await _mediator.Send(createVerificationCodeRequest, cancellationToken);

        return Ok(verificationState);
    }

    [HttpPost("verification-code/resend-alternative-email")]
    public async Task<IActionResult> ResendAlternativeEmailCodeRequestAsync(
        CancellationToken cancellationToken = default
    )
    {
        #region get user
        var getUserRequest = new DomainQueries.GetUserQuery(_userContext.UserId);

        var user = await _mediator.Send(getUserRequest, cancellationToken);
        #endregion

        var createVerificationCodeRequest = new DomainCommands
            .CreateAndSendVerificationCodeCommand(
                user.Email,
                Domain.Contracts.Enums.User.VerificationFieldType.AlternativeEmail
            );

        var verificationState = await _mediator.Send(createVerificationCodeRequest, cancellationToken);

        return Ok(verificationState);
    }
}
