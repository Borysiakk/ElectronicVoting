using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using ElectronicVoting.Infrastructure.Interface;
using ElectronicVoting.SignalR.Hubs;
using ElectronicVoting.SignalR.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PaillierCryptoSystem;
using PaillierCryptoSystem.Model;

namespace ElectronicVoting.Api.Controllers
{
    [ApiController]
    [Route("api/Elections")]
    public class ElectionsController:ControllerBase
    {
        private readonly KeyPrivate _keyPrivate;
        private readonly IElectionsService _electionsService;

        private readonly IHubContext<ValidationServerManagerHub,IConnectionClient> _hubContext;
        public ElectionsController(IElectionsService electionsService, KeyPrivate keyPrivate, IHubContext<ValidationServerManagerHub, IConnectionClient> hubContext)
        {
            _keyPrivate = keyPrivate;
            _hubContext = hubContext;
            _electionsService = electionsService;
        }

        [HttpGet("Candidates")]
        public ActionResult<List<Candidate>> GetCandidates()
        {
            return _electionsService.GetCandidates();
        }
        
        [HttpPost("Voice")]
        public async Task<IActionResult> Voice([FromBody]string voice)
        {
            try
            {
                Console.WriteLine("Nowy Głos");
                await _hubContext.Clients.Group("SuperValidator").ReceiveToSuperValidatorVoting(voice);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}