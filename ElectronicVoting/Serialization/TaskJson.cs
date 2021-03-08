using ElectronicVoting.PriorityQueue;

namespace ElectronicVoting.Serialization
{
    public class TaskJson
    {
        public string Id { get; set; }
        public Priority Priority { get; set; }
        public string TaskPacked { get; set; }
    }
}