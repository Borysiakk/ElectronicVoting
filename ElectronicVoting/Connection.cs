using System;
using System.Threading;
using ElectronicVoting.Domain.Contract.Result;
using ElectronicVoting.Interface;
using ElectronicVoting.PriorityQueue;
using ElectronicVoting.Serialization;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.MixedReality.WebRTC;
using Newtonsoft.Json;

namespace ElectronicVoting
{
    public class Connection :IConnectionValidator
    {
        private readonly CancellationToken _cancellationToken;
        public ManagementConnectionsValidation ManagementConnectionsValidation { get; }
        
        private HubConnection _hubConnection;
        public Connection()
        {
            _cancellationToken = new CancellationToken();
            ManagementConnectionsValidation = new ManagementConnectionsValidation();
        }

        public async System.Threading.Tasks.Task InitializationAsync(HttpAuthorizationResult authorization)
        {
            _hubConnection = new HubConnectionBuilder().WithUrl(Routes.SignalR.Connection, options =>
            {
                options.Headers.Add("Organization", authorization.Organization);
                options.Headers.Add("OrganizationId", authorization.OrganizationId);
                options.AccessTokenProvider = () => System.Threading.Tasks.Task.FromResult(authorization.Token);
            }).AddMessagePackProtocol().Build();
            
            _hubConnection.On("UpdateValidationServerList", async (string organization) =>
            {
                var peerConnection = await ManagementConnectionsValidation.Create(organization, _hubConnection);
                await peerConnection.AddDataChannelAsync("Low", false, true, _cancellationToken);
                await peerConnection.AddDataChannelAsync("Normal", false, true, _cancellationToken);
                await peerConnection.AddDataChannelAsync("High", false, true, _cancellationToken);
                bool result = peerConnection.CreateOffer();
            });
            
            _hubConnection.On("SdpMessageReceivedConfigurationWebRtc", async (string organization,SdpMessage message) =>
            {
                try
                {
                    var peerConnection = ManagementConnectionsValidation[organization];
                    
                    if (peerConnection == null)
                    {
                        var peer = await ManagementConnectionsValidation.Create(organization, _hubConnection);
                        await peer.SetRemoteDescriptionAsync(message);
                        if (message.Type == SdpMessageType.Offer)
                        {
                            peer.CreateAnswer();
                        }
                    }
                    else
                    {
                        await peerConnection.SetRemoteDescriptionAsync(message);
                        if (message.Type == SdpMessageType.Offer)
                        {
                            peerConnection.CreateAnswer();
                        }
                    }
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                
            });

            _hubConnection.On("IceCandidateReceivedConfigurationWebRtc", (string organization, IceCandidate iceCandidate) =>
            {
                var peer = ManagementConnectionsValidation[organization];
                peer.AddIceCandidate(iceCandidate);
            });

            _hubConnection.On("ReceiveToSuperValidatorVoting", (string text) =>
            {
                Console.WriteLine("Odbieranie głosu");
                TaskObject task = new TaskObject()
                {
                    Id = Guid.NewGuid().ToString(),
                    Operation = TaskOperation.Validation,
                    
                };
                ManagementConnectionsValidation.SendMessageToAll(task,PriorityMessage.Normal);
            });
            await _hubConnection.StartAsync(_cancellationToken);
        }

        public async System.Threading.Tasks.Task Close()
        {
            await _hubConnection.StopAsync(_cancellationToken);
            ManagementConnectionsValidation.Close();
        }
    }
}