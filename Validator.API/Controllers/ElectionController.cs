using MediatR;
using Microsoft.AspNetCore.Mvc;
using Validator.Infrastructure.Handler.Command.Election;

namespace Validator.API.Controllers;

[Route("api/Election/")]
public class ElectionController : BaseController
{
    public ElectionController(IMediator mediator) : base(mediator) { }

    [HttpPost("register-vote")]
    public async Task<IActionResult> RegisterVote(RegisterVote command, CancellationToken ct)
    {
        await _mediator.Send(command, ct);
        return Ok();
    }

    [HttpPost("initiate-voting-process")]
    public async Task<IActionResult> InitiateVotingProcess(InitiateVotingProcess command, CancellationToken ct)
    {
        await _mediator.Send(command, ct);
        return Ok();
    }

    [HttpPost("validate-votes")]
    public async Task<IActionResult> ValidateVotes(ValidateVotes command, CancellationToken ct)
    {
        await _mediator.Send(command, ct);
        return Ok();
    }

    [HttpPost("finalize-voting")]
    public async Task<IActionResult> FinalizeVoting(FinalizeVoting command, CancellationToken ct)
    {
        await _mediator.Send(command, ct);
        return Ok();
    }
}
