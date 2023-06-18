using MediatR;
using Microsoft.AspNetCore.Mvc;
using Validator.Domain.Handler.Command.Consensu;

namespace Validator.API.Controllers;
public class PbftConsensusController : BaseController
{
    public PbftConsensusController(IMediator mediator) : base(mediator)
    {
        Console.WriteLine("Start");
    }


    [HttpPost("Pre-Prepare")]
    public async Task<IActionResult> PrePrepare([FromQuery] PrePrepare command, CancellationToken ct)
    {
        await Mediator.Send(command, ct);
        return Ok();
    }

    [HttpPost("Prepare")]
    public async Task<IActionResult> Prepare(Prepare command, CancellationToken ct)
    {
        await Mediator.Send(command, ct);
        return Ok();
    }

    [HttpPost("Commit")]
    public async Task<IActionResult> Commit(Commit command, CancellationToken ct)
    {
        await Mediator.Send(command, ct);
        return Ok();
    }

    [HttpPost("Pre-Initialization-ChangeView")] 
    public async Task<IActionResult> PreInitializationChangeView(PreInitializationChangeView command, CancellationToken ct)
    {
        await Mediator.Send(command, ct);
        return Ok();
    }

    [HttpPost("Initialization-ChangeView")]
    public async Task<IActionResult> InitializationChangeView(InitializationChangeView command, CancellationToken ct)
    {
        await Mediator.Send(command, ct);
        return Ok();
    }

    [HttpPost("Commit-Initialization-ChangeView")]
    public async Task<IActionResult> CommitInitializationChangeView(CommitInitializationChangeView command, CancellationToken ct)
    {
        await Mediator.Send(command, ct);
        return Ok();
    }
}
