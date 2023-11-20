using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Validator.Application.Handler.Command.Election;

namespace Validator.API.Controllers
{
    public class ElectionController : BaseController
    {
        private readonly IBackgroundJobClient _backgroundJobClient;
        public ElectionController(IMediator mediator, IBackgroundJobClient backgroundJobClient) : base(mediator)
        {
            _backgroundJobClient = backgroundJobClient;
        }

        [HttpPost("register-vote")]
        public IActionResult RegisterVote([FromBody] RegisterVote command, CancellationToken ct)
        {
            _backgroundJobClient.Enqueue<IMediator>(a => a.Send(command, ct));
            return Ok();
        }

        [HttpPost("initiate-local-vote")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult InitiateLocalVote(InitiateLocalVote command, CancellationToken ct)
        {
            _backgroundJobClient.Enqueue<IMediator>(a => a.Send(command, ct));
            return Ok();
        }


        [HttpPost("verify-local-vote")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult VerifyLocalVote([FromBody] VerifyLocalVote command,CancellationToken ct)
        {
            _backgroundJobClient.Enqueue<IMediator>(a => a.Send(command, ct));
            return Ok();
        }

        [HttpPost("finalize-local-vote")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult FinalizeLocalVote([FromBody] FinalizeLocalVote command, CancellationToken ct)
        {
            _backgroundJobClient.Enqueue<IMediator>(a => a.Send(command, ct));
            return Ok();
        }

        [HttpPost("notify-local-vote-verification-completed")]
        public IActionResult NotifyLocalVoteVerificationCompleted([FromBody] NotifyLocalVoteVerificationCompleted command, CancellationToken ct)
        {
            _backgroundJobClient.Enqueue<IMediator>(a => a.Send(command, ct));
            return Ok();
        }

        [HttpPost("record-accepted-vote")]
        public IActionResult RecordAcceptedVote([FromBody] RecordAcceptedVote command, CancellationToken ct)
        {
            _backgroundJobClient.Enqueue<IMediator>(a => a.Send(command, ct));
            return Ok();
        }

    }
}
