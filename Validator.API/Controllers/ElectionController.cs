using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Validator.Infrastructure.Handler.Command.Election;

namespace Validator.API.Controllers;

[Route("api/Election/")]
public class ElectionController : BaseController
{
    private readonly IBackgroundJobClient _backgroundJobClient;

    public ElectionController(IMediator mediator, IBackgroundJobClient backgroundJobClient) : base(mediator)
    {
        _backgroundJobClient = backgroundJobClient;
    }

    [HttpPost("register-vote")]
    public async Task<IActionResult> RegisterVote(RegisterVote command, CancellationToken ct)
    {
        await _mediator.Send(command, ct);
        return Ok();
    }

    [HttpPost("initiate-voting-process")]
    public IActionResult InitiateVotingProcess(InitiateVotingProcess command, CancellationToken ct)
    {
        _backgroundJobClient.Enqueue<IMediator>(a => a.Send(command, ct));
        return Ok();
    }

    [HttpPost("validate-local-vote")]
    public IActionResult ValidateVote(ValidateLocalVote command, CancellationToken ct)
    {
        _backgroundJobClient.Enqueue<IMediator>(a => a.Send(command, ct));
        return Ok();
    }

    [HttpPost("finalize-local-voting")]
    public IActionResult FinalizeVoting(FinalizeLocalVoting command, CancellationToken ct)
    {
        _backgroundJobClient.Enqueue<IMediator>(a => a.Send(command, ct));
        return Ok();
    }

    [HttpPost("notify-local-voting-completed")]
    public IActionResult NotifyLocalVotingCompleted(NotifyLocalVotingCompleted command, CancellationToken ct)
    {
        _backgroundJobClient.Enqueue<IMediator>(a => a.Send(command, ct));
        return Ok();
    }

    [HttpPost("record-accepted-vote")]
    public IActionResult RecordAcceptedVote(RecordAcceptedVote command, CancellationToken ct)
    {
        _backgroundJobClient.Enqueue<IMediator>(a => a.Send(command, ct));
        return Ok();
    }
}
