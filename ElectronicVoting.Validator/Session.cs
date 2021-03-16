using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ElectronicVoting.Builder;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.MixedReality.WebRTC;

namespace ElectronicVoting
{
    public class Session
    {
        private Dictionary<string, PeerConnection> _peers;

        public PeerConnection this[string organization] => _peers.ContainsKey(organization) ? _peers[organization] : null;

        public Session()
        {
            _peers = new Dictionary<string, PeerConnection>();
        }

        public void Close()
        {
            foreach (var peer in _peers.Values)
            {
                peer.Close();
            }
        }
        
        public async Task<PeerConnection> Create(string organization, HubConnection hubConnection)
        {
            PeerConnection peerConnection = await PeerBuilder.Create(organization, hubConnection);
            
            peerConnection.DataChannelAdded += channel =>
            {
                channel.MessageReceived += bytes =>
                {
                    Console.WriteLine(Encoding.ASCII.GetString(bytes));
                };
            };
            
            _peers.Add(organization, peerConnection);
        
            return peerConnection;
        }

        public async void Send(string message)
        {
            foreach (var peer in _peers.Values)
            {
                byte[] bytes = Encoding.ASCII.GetBytes(message);  
                peer.DataChannels[0].SendMessage(bytes);
            }
        }
    }
}