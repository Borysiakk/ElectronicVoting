using Main.Dto;
using Main.Infrastructure.Handler;
using Main.Infrastructure.Handler.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Main.API.Controllers;

public class LeaderController : BaseController
{
    public LeaderController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("current-leader")]
    public async Task<ActionResult<LeaderDto>> GetCurrentLeader(GetCurrentLeader query, CancellationToken ct)
    {
        return Ok(await _mediator.Send(query, ct));
    }

    [HttpPost("current-leader")]
    public async Task<ActionResult> SetCurrentLeader(SetCurrentLeader command, CancellationToken ct)
    {
        await _mediator.Send(command, ct);
        return Ok();
    }
}
