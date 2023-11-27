using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Main.API.Controller;

[Route("api/[controller]")]
public class BaseController : ControllerBase
{
    protected readonly IMediator Mediator;

    public BaseController(IMediator mediator)
    {
        Mediator = mediator;
    }
}
