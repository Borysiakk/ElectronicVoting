using System.Threading.Tasks;
using Microsoft.MixedReality.WebRTC;

namespace ElectronicVoting.SignalR.Interface
{
    public interface IConfigurationValidationClient
    {
        public Task SdpMessageReceivedConfigurationWebRtc(string organization,SdpMessage sdpMessage);
        public Task IceCandidateReceivedConfigurationWebRtc(string organization, IceCandidate iceCandidate);
    }
}