﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using static System.Collections.Specialized.BitVector32;

namespace Validator.API.Controllers
{
    [Route("api/Election/ChangeLeader")]
    public class ChangeLeaderController : BaseController
    {
        public ChangeLeaderController(IMediator mediator) : base(mediator) {}

        [HttpPost("init-pre-election-preparation")]
        public IActionResult InitPreElectionPreparation(CancellationToken cancellationToken)
        {
            return Ok();
        }

        [HttpPost("pre-election-preparation")]
        public ActionResult PreElectionPreparation(CancellationToken ct)
        {
            return Ok();
        }
    }
}