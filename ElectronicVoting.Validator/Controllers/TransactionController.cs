using ElectronicVoting.API.Handler.Command.Transaction;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicVoting.API.Controllers
{
    public class TransactionController : BaseController
    {
        public TransactionController(IMediator mediator) : base(mediator) {}

        [HttpPost("Add")]
        public async Task<IActionResult> Add(AddRegisteredTransaction command, CancellationToken ct)
        {
            await Mediator.Send(command, ct);

            return Ok();
        }
    }
}
