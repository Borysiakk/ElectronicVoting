using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.MixedReality.WebRTC;

namespace ElectronicVoting.Validator.Factory
{
    public static class PeerConnectionFactory
    {
        public static async Task<PeerConnection> Create(ConnectionConfiguration configuration) 
        {
            PeerConnection peerConnection = new PeerConnection();
            
            peerConnection.LocalSdpReadytoSend += configuration.SdpReadyToSend;
            peerConnection.DataChannelAdded += configuration.DataChannelAddedDelegate;
            peerConnection.IceCandidateReadytoSend += configuration.IceCandidateReadyToSend;
            
            var config = new PeerConnectionConfiguration
            {
                IceServers = new List<IceServer> {new IceServer{ Urls = { "stun:stun.l.google.com:19302" } } }
            };
            
            await peerConnection.InitializeAsync(config);

            return peerConnection;
        }
    }
}