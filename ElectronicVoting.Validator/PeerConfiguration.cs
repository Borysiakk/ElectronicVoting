using Microsoft.MixedReality.WebRTC;

namespace ElectronicVoting
{
    public class PeerConfiguration
    {
        public PeerConnection.LocalSdpReadyToSendDelegate SdpReadyToSend { get; set; }
        public PeerConnection.IceCandidateReadytoSendDelegate IceCandidateReadyToSend { get; set; }
        public PeerConnection.IceGatheringStateChangedDelegate IceGatheringStateChanged { get; set; }
    }
}