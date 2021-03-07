using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ElectronicVoting.Builder;
using ElectronicVoting.Interface;
using KolejkaPriorytetowa;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.MixedReality.WebRTC;

namespace ElectronicVoting
{
    public class ManagementConnectionsValidation :IManagementConnectionsValidation
    {
        private Dictionary<string, PeerConnection> _peers;
        private readonly PriorityQueueAsync<NodePriorityQueue> _priorityQueue;
        public PeerConnection this[string organization] => _peers.ContainsKey(organization) ? _peers[organization] : null;

        public ManagementConnectionsValidation()
        {
            _peers = new Dictionary<string, PeerConnection>();
            _priorityQueue = new PriorityQueueAsync<NodePriorityQueue>();
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
        
        public void SendMessage(string organization, byte[] message, PriorityMessage priority = PriorityMessage.Normal)
        {
            throw new System.NotImplementedException();
        }
        
        public void SendMessageToAll(byte[] message, PriorityMessage priority = PriorityMessage.Normal)
        {
            throw new System.NotImplementedException();
        }
        
        public void Close()
        {
            foreach (var peer in _peers.Values)
            {
                peer.Close();
            }
        }
    }
}