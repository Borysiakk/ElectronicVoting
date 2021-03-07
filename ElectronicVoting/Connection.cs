using System;
using System.Threading;
using ElectronicVoting.Domain.Contract.Result;
using ElectronicVoting.Interface;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.MixedReality.WebRTC;

namespace ElectronicVoting
{
    public class Connection :IConnectionValidator
    {
        public ManagementConnectionsValidation ManagementConnectionsValidation { get; }
        
        private HubConnection _hubConnection;
        public Connection()
        {
            ManagementConnectionsValidation = new ManagementConnectionsValidation();
        }

        public async System.Threading.Tasks.Task InitializationAsync(HttpAuthorizationResult authorization)
        {
            _hubConnection = new HubConnectionBuilder().WithUrl(Routes.SignalR.Connection, options =>
            {
                options.Headers.Add("Organization", authorization.Organization);
                options.AccessTokenProvider = () => System.Threading.Tasks.Task.FromResult(authorization.Token);
            }).AddMessagePackProtocol().Build();
            
            _hubConnection.On("UpdateValidationServerList", async (string organization) =>
            {
                var peerConnection = await ManagementConnectionsValidation.Create(organization, _hubConnection);
                await peerConnection.AddDataChannelAsync(Guid.NewGuid().ToString(), true, true, new CancellationToken());
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
            await _hubConnection.StartAsync();
        }

        public async System.Threading.Tasks.Task Close()
        {
            await _hubConnection.StopAsync();
            ManagementConnectionsValidation.Close();
        }
    }
}