using System.Collections.Generic;
using Microsoft.MixedReality.WebRTC;

namespace ElectronicVoting
{
    public class Peer
    {
        public PeerConnection PeerConnection { get; set; }
        private PeerConnectionConfiguration _peerConnectionConfiguration;
        
        public Peer(PeerConfiguration peerConfiguration)
        {
            _peerConnectionConfiguration = new PeerConnectionConfiguration
            {
                IceServers = new List<IceServer> { new IceServer{ Urls = { "stun:stun.l.google.com:19302" } }}
            };
        }
        
        public Peer(PeerConfiguration peerConfiguration,PeerConnectionConfiguration connectionConfiguration)
        {
            _peerConnectionConfiguration = connectionConfiguration;
        }
    }
}