using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ElectronicVoting.Domain.Entities;
using ElectronicVoting.Infrastructure.Interface;
using ElectronicVoting.SignalR.Interface;
using Microsoft.AspNetCore.SignalR;
using Microsoft.MixedReality.WebRTC;

namespace ElectronicVoting.SignalR.Hubs
{
    public class ValidationServerManagerHub:Hub<IConnectionClient>
    {

        private readonly ISessionValidatorService _sessionValidatorService;

        public ValidationServerManagerHub(ISessionValidatorService sessionValidatorService)
        {
            _sessionValidatorService = sessionValidatorService;
        }

        public override async Task OnConnectedAsync()
        {
            var organization = Context.GetHttpContext().Request.Headers["Organization"].ToString();
            var organizationId = Context.GetHttpContext().Request.Headers["OrganizationId"].ToString();
            
            SessionValidator sessionValidator = new SessionValidator()
            {
                StatusConnection = true,
                Organization = organization,
                OrganizationId = organizationId,
                ConnectionId = Context.ConnectionId,
            };
            
            await _sessionValidatorService.AddAsync(sessionValidator);
            await Clients.Others.UpdateValidationServerList(organization);

            if (organization == "szymaborys@gmail.com")
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "SuperValidator");
            }
            
            Console.WriteLine("Login");
            Console.WriteLine("Organization {0}",organization);
            Console.WriteLine("Organization {0}",organizationId);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            Console.WriteLine("Logut");
            await _sessionValidatorService.CloseAsync(Context.ConnectionId);
        }
        
        public async Task SdpMessageSendConfigurationWebRtc(string organization, SdpMessage message)
        {
            Console.WriteLine("Wysyłanie oferty SDP");
            string connectionId =  _sessionValidatorService.GetConnectionIdByOrganization(organization);
            string organizationCurrent = _sessionValidatorService.GetOrganizationByConnectionId(Context.ConnectionId);
            
            await Clients.Client(connectionId).SdpMessageReceivedConfigurationWebRtc(organizationCurrent, message);
        }
        
        public async Task IceCandidateSendConfigurationWebRtc(string organization, IceCandidate candidate)
        {
            string connectionId =  _sessionValidatorService.GetConnectionIdByOrganization(organization);
            string organizationCurrent = _sessionValidatorService.GetOrganizationByConnectionId(Context.ConnectionId);
            
            await Clients.Client(connectionId).IceCandidateReceivedConfigurationWebRtc(organizationCurrent, candidate);
        }
    }
}