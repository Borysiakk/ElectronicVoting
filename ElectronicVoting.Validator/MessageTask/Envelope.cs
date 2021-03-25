using ElectronicVoting.Validator.PriorityQueue;

namespace ElectronicVoting.Validator.MessageTask
{
    public class Envelope
    {
        public string Task { get; set; }
        public PriorityMessage Priority { get; set; }
        
    }
}