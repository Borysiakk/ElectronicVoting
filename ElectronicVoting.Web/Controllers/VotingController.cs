using ElectronicVoting.SignalR.Hubs;
using ElectronicVoting.SignalR.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ElectronicVoting.Web.Controllers
{
    public class VotingController :Controller
    {
        private readonly IHubContext<ValidationServerManagerHub,IConnectionClient> _hubContext;

        public VotingController(IHubContext<ValidationServerManagerHub, IConnectionClient> hubContext)
        {
            _hubContext = hubContext;
        }

        public IActionResult Index()
        {
            
            _hubContext.Clients.Group("SuperValidator").ReceiveToSuperValidatorVoting("TEST");
            return View();
        }
    }
}