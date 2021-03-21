using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ElectronicVoting.Validator.MessageTask;
using ElectronicVoting.Validator.MessageTask.Serialization;
using ElectronicVoting.Validator.PriorityQueue;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.MixedReality.WebRTC;

namespace ElectronicVoting.Validator
{
    public partial class ConnectionManagement
    {
        private HubConnection _hubConnection;
        private CancellationToken _cancellationToken;
        private Dictionary<string, PeerConnection> _peers;
        private PriorityQueueAsync<NodePriorityQueue> _priorityQueueAsync;

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
        
    }
}