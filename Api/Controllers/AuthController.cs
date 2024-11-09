using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Api.Contracts.Requests.User;
using Domain.Contracts.Commands;
using Domain.Contracts.Dtos;
using Domain.Contracts.Queries;
using Api.Contracts.Requests.VerificationCode;


namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;

    private readonly IMediator _mediator;

    private readonly IMapper _mapper;

    public AuthController(
        ILogger<AuthController> logger,
        IMediator mediator,
        IMapper mapper
    )
    {
        _logger = logger;

        _mapper = mapper;

        _mediator = mediator;
    }

    [HttpPost("sign-in")]
    [Produces("application/json")]
    [ProducesResponseType<UserDto>(StatusCodes.Status200OK)]
    public async Task<IActionResult> SignInRequestAsync([FromBody] SignInRequest request, CancellationToken cancellationToken = default)
    {
        var signInRequest = _mapper.Map<SignInQuery>(request);

        var signInResponse = await _mediator.Send(signInRequest, cancellationToken);

        SetupSecurityToken(signInResponse.User, signInResponse.SecurityToken);

        return Ok(signInResponse.User);
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUpRequestAsync([FromBody] SignUpRequest request, CancellationToken cancellationToken = default)
    {
        var signUpRequest = _mapper.Map<SignUpCommand>(request);

        var signUpResponse = await _mediator.Send(signUpRequest, cancellationToken);

        SetupSecurityToken(signUpResponse.User, signUpResponse.SecurityToken);

        return Ok(signUpResponse.User);
    }

    [HttpPost("setup-password")]
    public async Task<IActionResult> SetupPasswordRequestAsync([FromBody] SetupPasswordRequest request, CancellationToken cancellationToken = default)
    {
        var setupPasswordRequest = _mapper.Map<SetupPasswordCommand>(request);

        var setupPasswordResponse = await _mediator.Send(setupPasswordRequest, cancellationToken);

        if (setupPasswordResponse)
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpPost("confirm-email")]
    public async Task<IActionResult> SetupPasswordRequestAsync([FromBody] ConfirmVerificationCodeRequest request, CancellationToken cancellationToken = default)
    {
        var confirmVerificationCodeRequest = _mapper.Map<ConfirmVerificationCodeCommand>(request);

        var confirmVerificationCodeResponse = await _mediator.Send(confirmVerificationCodeRequest, cancellationToken);

        if (confirmVerificationCodeResponse)
        {
            return Ok();
        }

        return BadRequest();
    }

    private void SetupSecurityToken(UserDto user, SecurityTokenDto securityToken)
    {
        var domain = HttpContext.Request.Host.Host;

        var domainSegments = domain.Split('.');

        if (domainSegments.Length > 1)
        {
            domain = "." + string.Join(".", domainSegments.Skip(1));
        }

        var options = new CookieOptions
        {
            HttpOnly = true,
            Domain = domain
        };

        HttpContext.Response.Cookies.Append("UserId", user.UserId.ToString(), options);
        HttpContext.Response.Cookies.Append("Token", securityToken.SecurityToken, options);
        HttpContext.Response.Cookies.Append("RefreshToken", securityToken.RefreshToken, options);
    }
}
