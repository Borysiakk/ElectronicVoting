using Main.Application.Handler.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Main.API.Controller;

[ApiController]
[Route("api/[controller]")]
public class CandidateController : BaseController
{
    public CandidateController(IMediator mediator) : base(mediator){}

    [HttpPost("add")]
    public async Task<IActionResult> AddCandidate(AddCandidate addCandidate, CancellationToken cancellationToken)
    {
        return Ok();
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetCandidates(CancellationToken cancellationToken)
    {

    }
}
