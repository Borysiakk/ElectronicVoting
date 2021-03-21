using ElectronicVoting.Validator.PriorityQueue;

namespace ElectronicVoting.Validator.MessageTask
{
    public class Envelope
    {
        public string Id { get; set; }
        public PriorityMessage Priority { get; set; }
        public string Task { get; set; }
    }
}