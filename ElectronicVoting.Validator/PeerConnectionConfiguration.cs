using Microsoft.MixedReality.WebRTC;

namespace ElectronicVoting.Validator
{
    public class ConnectionConfiguration
    {
        public PeerConnection.LocalSdpReadyToSendDelegate SdpReadyToSend { get; set; }
        public PeerConnection.DataChannelAddedDelegate DataChannelAddedDelegate { get; set; }
        public PeerConnection.IceCandidateReadytoSendDelegate IceCandidateReadyToSend { get; set; }
        public PeerConnection.IceGatheringStateChangedDelegate IceGatheringStateChanged { get; set; }
    }
}