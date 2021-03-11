using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ElectronicVoting.Builder;
using ElectronicVoting.Interface;
using ElectronicVoting.PriorityQueue;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.MixedReality.WebRTC;

namespace ElectronicVoting
{
    public class ManagementConnectionsValidation :IManagementConnectionsValidation
    {
        private Dictionary<string, PeerConnection> _peers;
        public readonly PriorityQueueAsync<NodePriorityQueue> PriorityQueue;
        public PeerConnection this[string organization] => _peers.ContainsKey(organization) ? _peers[organization] : null;

        public ManagementConnectionsValidation()
        {
            _peers = new Dictionary<string, PeerConnection>();
            PriorityQueue = new PriorityQueueAsync<NodePriorityQueue>();

            PriorityQueue.ActionAutoReadNode = async () =>
            {
                while (PriorityQueue.IsEmpty())
                {
                    NodePriorityQueue node = await PriorityQueue.Pop();
                    
                }
            };
        }

        public async Task<PeerConnection> Create(string organization, HubConnection hubConnection)
        {
            PeerConnection peerConnection = await PeerBuilder.Create(organization, hubConnection);
            
            peerConnection.DataChannelAdded += channel =>
            {
                channel.MessageReceived += bytes =>
                {
                    var task = SerializationTask.DeserializePreparatory(bytes);
                    PriorityQueue.Push(task.Value,task.Key);
                    
                    Console.WriteLine("Priorytet o ważności {0}",task.Key);
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