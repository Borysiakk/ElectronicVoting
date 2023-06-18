using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Validator.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected readonly IMediator Mediator;

    public BaseController(IMediator mediator)
    {
        Mediator = mediator;
    }
}
