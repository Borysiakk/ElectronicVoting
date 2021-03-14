using ElectronicVoting.PriorityQueue;

namespace ElectronicVoting.Serialization
{
    public class TaskIntroductory
    {
        public string Id { get; set; }
        public PriorityMessage Priority { get; set; }
        
        public string Task { get; set; }
    }
}