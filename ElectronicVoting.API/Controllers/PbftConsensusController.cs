using ElectronicVoting.API.Handler.Command.PbftConsensus;
using ElectronicVoting.Domain.Handler.Command.Consensu;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicVoting.API.Controllers
{
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
    }
}
