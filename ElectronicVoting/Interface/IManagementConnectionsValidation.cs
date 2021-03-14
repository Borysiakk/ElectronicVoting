using ElectronicVoting.PriorityQueue;
using ElectronicVoting.Serialization;

namespace ElectronicVoting.Interface
{
    
    public interface IManagementConnectionsValidation
    {
        public void SendMessageToAll(TaskObject task,PriorityMessage priority = PriorityMessage.Normal);
        public void SendMessage(string organization, string task,PriorityMessage priority = PriorityMessage.Normal);
    }
}