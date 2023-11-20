using MediatR;
using Microsoft.AspNetCore.Mvc;
using Validator.Application.Handler.Query.Blockchain;

namespace Validator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlockchainController : BaseController
    {
        public BlockchainController(IMediator mediator) : base(mediator){}

        [HttpPost("block/all")]
        public async Task<IActionResult> GetBlocks(GetBlocks query, CancellationToken ct)
        {
            return Ok(await Mediator.Send(query, ct));
        }

        [HttpPost("transaction/all")]
        public async Task<IActionResult> GetTransactions(GetTransactions query, CancellationToken ct)
        {
            return Ok(await Mediator.Send(query, ct));
        }
    }
}
