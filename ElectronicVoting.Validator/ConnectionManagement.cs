using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ElectronicVoting.Validator.Consensus;
using ElectronicVoting.Validator.Interface;
using ElectronicVoting.Validator.MessageTask;
using ElectronicVoting.Validator.MessageTask.Serialization;
using ElectronicVoting.Validator.PriorityQueue;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.MixedReality.WebRTC;

namespace ElectronicVoting.Validator
{
    public partial class ConnectionManagement :IConnectionManagement
    {
        public Blockchain Blockchain;
        private HubConnection _hubConnection;
        private CancellationToken _cancellationToken;
        private Dictionary<string, PeerConnection> _peers;
        private PriorityQueueAsync<NodePriorityQueue> _priorityQueueAsync;

        public string Organization { get; set; }
        
        private ConnectionManagement()
        {
            
        }

        public async Task StartAsync()
        {
            await _hubConnection.StartAsync(_cancellationToken);
        }

        public async Task StopAsync()
        {
            await _hubConnection.StopAsync(_cancellationToken);
            foreach (var peerConnection in _peers.Values)
            {
                peerConnection.Close();
            }
        }

        public List<string> GetAllConnectionValidatorsName()
        {
            var list = _peers.Keys.ToList();
            list.Add(Organization);
            return list;
        }

        public List<string> GetCurrentConnectionValidatorName()
        {
            return _peers.Keys.ToList();
        }
        
        public void SendMessages(string validator,byte [] msg, PriorityMessage priority)
        {
            try
            {
                _peers[validator].DataChannels[(int)priority].SendMessage(msg);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}