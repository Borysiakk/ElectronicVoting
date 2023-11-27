using Main.Application.Handler.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Main.API.Controller;

[ApiController]
[Route("api/[controller]")]
public class CandidateController : BaseController
{
    public CandidateController(IMediator mediator) : base(mediator){}

    [HttpPost("")]
    Task<IActionResult> AddCandidate(AddCandidate addCandidate, CancellationToken cancellationToken)
    {

    }
}
