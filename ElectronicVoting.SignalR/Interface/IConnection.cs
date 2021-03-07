using System.Threading.Tasks;
using Microsoft.MixedReality.WebRTC;

namespace ElectronicVoting.SignalR.Interface
{
    public interface IConnection :IConfigurationValidationClient
    {
        public Task UpdateValidationServerList(string organization);
    }
}