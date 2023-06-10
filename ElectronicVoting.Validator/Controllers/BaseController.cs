using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicVoting.API.Controllers
{
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
}
