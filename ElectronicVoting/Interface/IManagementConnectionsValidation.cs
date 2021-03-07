namespace ElectronicVoting.Interface
{

    public enum PriorityMessage
    {
        Low,
        Normal,
        High
    }
    
    public interface IManagementConnectionsValidation
    {
        public void SendMessageToAll(byte[] message,PriorityMessage priority = PriorityMessage.Normal);
        public void SendMessage(string organization, byte[] message,PriorityMessage priority = PriorityMessage.Normal);
    }
}