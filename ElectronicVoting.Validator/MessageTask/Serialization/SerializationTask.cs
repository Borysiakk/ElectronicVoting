using System.Collections.Generic;
using ElectronicVoting.Validator.PriorityQueue;
using Newtonsoft.Json;

namespace ElectronicVoting.Validator.MessageTask.Serialization
{
    public class SerializationTask
    {
        public static byte[] SerializationEnvelope(Envelope envelope)
        {
            string json = JsonConvert.SerializeObject(envelope);
            return System.Text.Encoding.UTF8.GetBytes(json);
        }
        
        public static KeyValuePair<PriorityMessage,NodePriorityQueue> DeserializeEnvelope(byte[] bytes)
        {
            string json = System.Text.Encoding.UTF8.GetString(bytes);
            var task = JsonConvert.DeserializeObject<Envelope>(json);

            NodePriorityQueue nodePriorityQueue = new NodePriorityQueue()
            {
                Task = task.Task,
                Priority = task.Priority,
            };
            
            return new KeyValuePair<PriorityMessage, NodePriorityQueue>(task.Priority,nodePriorityQueue);
        }
    }
}