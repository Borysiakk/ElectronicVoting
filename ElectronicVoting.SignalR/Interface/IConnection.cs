using System.Threading.Tasks;
using Microsoft.MixedReality.WebRTC;

namespace ElectronicVoting.SignalR.Interface
{
    public interface IConnectionClient :IConfigurationValidationClient,IConnectionVotingClient
    {
        public Task UpdateValidationServerList(string organization);
    }
}