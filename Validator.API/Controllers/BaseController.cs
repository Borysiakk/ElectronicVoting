using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Validator.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseController : ControllerBase
{
    protected readonly IMediator _mediator;
    public BaseController(IMediator mediator)
    {
        _mediator = mediator;
    }
}