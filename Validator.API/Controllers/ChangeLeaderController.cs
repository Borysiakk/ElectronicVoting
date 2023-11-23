using MediatR;
using Microsoft.AspNetCore.Mvc;
using Validator.Application.Handler.Command.ChangeLeader;

namespace Validator.API.Controllers
{
    [Route("api/Election/ChangeLeader")]
    public class ChangeLeaderController : BaseController
    {
        public ChangeLeaderController(IMediator mediator) : base(mediator) {}

        [HttpPost("init-pre-election-preparation")]
        public IActionResult InitPreElectionPreparation(InitPreElectionPreparation command, CancellationToken cancellationToken)
        {
            return Ok();
        }

        [HttpPost("pre-election-preparation")]
        public ActionResult PreElectionPreparation(PreElectionPreparation command, CancellationToken ct)
        {
            return Ok();
        }

        [HttpPost("leadership-change-assessment")]
        public IActionResult LeadershipChangeAssessment(CancellationToken ct)
        {

        }
    }
}