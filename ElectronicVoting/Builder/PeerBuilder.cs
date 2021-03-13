using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.MixedReality.WebRTC;

namespace ElectronicVoting.Builder
{
    public static class PeerBuilder
    {
        public static async Task<PeerConnection> Create(string organization,HubConnection hubConnection)
        {
            PeerConnection peerConnection = new PeerConnection()
            {
                Name = organization
            };
        
            peerConnection.LocalSdpReadytoSend += async message =>
            {
                Console.WriteLine("Wysłanie konfiguracji SDP");
                await hubConnection.InvokeAsync("SdpMessageSendConfigurationWebRtc",peerConnection.Name, message);
            };

            peerConnection.IceCandidateReadytoSend += async candidate =>
            {
                await hubConnection.InvokeAsync("IceCandidateSendConfigurationWebRtc",peerConnection.Name, candidate);
            };

            var config = new PeerConnectionConfiguration
            {
                IceServers = new List<IceServer> {new IceServer{ Urls = { "stun:stun.l.google.com:19302" } } }
            };
            await peerConnection.InitializeAsync(config);
            
            return peerConnection;
        }
    }
}