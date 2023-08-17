using MediatR;
using Microsoft.AspNetCore.Mvc;
using Validator.Infrastructure.Handler.Query.Blockchain;

namespace Validator.API.Controllers
{
    [Route("api/Blockchain/")]
    public class BlockchainController : BaseController
    {
        public BlockchainController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("block/all")]
        public async Task<IActionResult> GetBlocks(GetBlocks query,CancellationToken ct)
        {
            return Ok(await _mediator.Send(query, ct));
        }

    }
}
