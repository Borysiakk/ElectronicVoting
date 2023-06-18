using MediatR;
using Microsoft.AspNetCore.Mvc;
using Validator.Domain.Handler.Command;

namespace Validator.API.Controllers;
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
