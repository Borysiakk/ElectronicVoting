using MediatR;
using Microsoft.AspNetCore.Mvc;
using Validator.API.Handler.Command.PbftConsensus.ChangeLeader;
using Validator.Domain.Handler.Command.Consensu.ChangeLeader;

namespace Validator.API.Controllers
{
    [Route("api/PbftConsensus/ChangeLeader")]
    public class ChangeLeader : BaseController
    {
        public ChangeLeader(IMediator mediator) : base(mediator){}

        [HttpPost("pre-election-preparation")]
        public async Task<IActionResult> PreElectionPreparation(PreElectionPreparation command, CancellationToken ct)
        {
            await Mediator.Send(command, ct);
            return Ok();
        }

        [HttpPost("pre-election-vote-record")]
        public async Task<IActionResult> PreElectionVoteRecord(PreElectionVoteRecord command, CancellationToken ct)
        {
            await Mediator.Send(command, ct);
            return Ok();
        }

        [HttpPost("election-vote-record")]
        public async Task<IActionResult> ElectionVoteRecord(ElectionVoteRecord command, CancellationToken ct)
        {
            await Mediator.Send(command, ct);
            return Ok();
        }
    }
}
