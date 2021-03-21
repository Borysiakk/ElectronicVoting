namespace ElectronicVoting.Validator.MessageTask
{
    public class TaskObject
    {
        public string Id { get; set; }
        public TaskOperation Operation { get; set; }
        public string Data { get; set; }
    }
}