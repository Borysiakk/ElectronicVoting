using MediatR;
using Microsoft.AspNetCore.Mvc;
using Validator.Infrastructure.Handler.Command.ChangeLeader;

namespace Validator.API.Controllers;

[Route("api/Election/ChangeLeader")]
public class ChangeLeaderController : BaseController
{
    public ChangeLeaderController(IMediator mediator) : base(mediator){}

    [HttpPost("pre-election-preparation-initialization")]
    public async Task<IActionResult> PreElectionPreparationInitChangeLeader(PreElectionPreparationInitChangeLeader command, CancellationToken ct)
    {
        await _mediator.Send(command, ct);
        return Ok();
    }

    [HttpPost("pre-election-preparation")]
    public async Task<IActionResult> PreElectionPreparationChangeLeader(PreElectionPreparation command, CancellationToken ct)
    {
        await _mediator.Send(command, ct);
        return Ok();
    }

    [HttpPost("pre-election-vote-record")]
    public async Task<IActionResult> PreElectionVoteRecordChangeLeader(PreElectionVoteRecordChangeLeader command ,CancellationToken ct)
    {
        await _mediator.Send(command, ct);
        return Ok();
    }

    [HttpPost("pre-election-notify-leader-completed")]
    public async Task<IActionResult> PreElectionChangeLeaderNotifyLeaderCompleted(PreElectionChangeLeaderNotifyLeaderCompleted command, CancellationToken ct)
    {
        await _mediator.Send(command, ct);
        return Ok();
    }

    [HttpPost("election-preparation-initialization")]
    public async Task<IActionResult> ElectionPreparationInitChangeLeader(ElectionPreparationInitChangeLeader command, CancellationToken ct)
    {
        await _mediator.Send(command, ct);
        return Ok();
    }

    [HttpPost("election-preparation")]
    public async Task<IActionResult> ElectionPreparation(ElectionPreparationChangeLeader command, CancellationToken ct)
    {
        await _mediator.Send(command, ct);
        return Ok();
    }

    [HttpPost("election-vote-record")]
    public async Task<IActionResult> ElectionVoteRecordChangeLeader(ElectionVoteRecordChangeLeader command, CancellationToken ct)
    {
        await _mediator.Send(command, ct);
        return Ok();
    }

    [HttpPost("election-notify-leader-completed")]
    public async Task<IActionResult> NotifyElectionLeaderCompleted(ElectionChangeLeaderNotifyLeaderCompleted command, CancellationToken ct)
    {
        await _mediator.Send(command, ct);
        return Ok();
    }

    [HttpPost("set-new-leader")]
    public async Task<IActionResult> SetNewLeader(SetNewLeader command, CancellationToken ct)
    {
        await _mediator.Send(command, ct);
        return Ok();
    }



}
