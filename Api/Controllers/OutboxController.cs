using MediatR;
using Api.Contracts.Requests.Outbox;
using Infrastructure.Contracts.Commands;
using Infrastructure.Contracts.Enums.Outbox;
using Infrastructure.Contracts.Queries;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Api.Attributes.Authorization.Outbox;


namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
[OutboxAuth]
public class OutboxController : Controller
{
    private readonly ILogger<OutboxController> _logger;

    private readonly IMediator _mediator;

    public OutboxController(
        ILogger<OutboxController> logger,
        IMediator mediator
    )
    {
        _logger = logger;

        _mediator = mediator;
    }

    [HttpPost("message/next")]
    public async Task<IActionResult> NextMessageAsync([FromQuery, Required] MessageType messageType, CancellationToken cancellationToken = default)
    {
        var hc = HttpContext;

        var nextMessageRequest = new NextOutboxMessageCommand(messageType);

        var nextMessageResponse = await _mediator.Send(nextMessageRequest, cancellationToken);

        if (nextMessageResponse != null)
        {
            return Ok(nextMessageResponse);
        }

        return NoContent();
    }

    [HttpPut("message/{messageId}/success")]
    public async Task<IActionResult> SetMessageSuccessStatusAsync(
        Guid messageId,
        CancellationToken cancellationToken = default
    )
    {
        var setMessageSuccessStatusRequest = new SetOutboxMessageSuccessStatusCommand(messageId);

        await _mediator.Send(setMessageSuccessStatusRequest, cancellationToken);

        return NoContent();
    }

    [HttpPut("message/{messageId}/error")]
    public async Task<IActionResult> SetMessageErrorStatusAsync(
        Guid messageId,
        [FromBody] SetOutboxMessageErrorRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var setMessageErrorStatusRequest = new SetOutboxMessageErrorStatusCommand(messageId, request.Error);

        await _mediator.Send(setMessageErrorStatusRequest, cancellationToken);

        return NoContent();
    }

    [HttpGet("message/processed-list")]
    public async Task<IActionResult> GetProcessedMessagesAsync(CancellationToken cancellationToken)
    {
        var getProcessedOutboxMessagesRequest = new GetProcessedOutboxMessagesQuery();

        var messageIds = await _mediator.Send(getProcessedOutboxMessagesRequest, cancellationToken);

        if (messageIds.Any())
        {
            return Ok(messageIds);
        }

        return NoContent();
    }

    [HttpDelete("message")]
    public async Task<IActionResult> RemoveProcessedMessages(
        [FromBody] RemoveProcessedMessagesRequest request,
        CancellationToken cancellationToken
    )
    {
        var removeProcessedOutboxMessagesRequest = new RemoveProcessedOutboxMessagesCommand(request.MessageIds);

        await _mediator.Send(removeProcessedOutboxMessagesRequest, cancellationToken);

        return NoContent();
    }
}
