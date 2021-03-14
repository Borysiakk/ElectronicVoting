using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ElectronicVoting.PriorityQueue;
using ElectronicVoting.Serialization;
using Newtonsoft.Json;

namespace ElectronicVoting
{
    public class SerializationTask
    {
        public static KeyValuePair<PriorityMessage,NodePriorityQueue> SerializePreparatory(byte[] bytes)
        {
            string json = System.Text.Encoding.UTF8.GetString(bytes);
            var task = JsonConvert.DeserializeObject<TaskIntroductory>(json);

            return new KeyValuePair<PriorityMessage, NodePriorityQueue> (task.Priority,new NodePriorityQueue() {TaskJson = task.Task});
        }

        
        public static KeyValuePair<PriorityMessage,NodePriorityQueue> DeserializePreparatory(byte[] bytes)
        {
            string json = System.Text.Encoding.UTF8.GetString(bytes);
            var task = JsonConvert.DeserializeObject<TaskIntroductory>(json);

            return new KeyValuePair<PriorityMessage, NodePriorityQueue>(task.Priority,new NodePriorityQueue() {TaskJson = task.Task});
        }

        public async Task<TaskObject> DeserializeTask(string task)
        {
            return await Task.Run(() => JsonConvert.DeserializeObject<TaskObject>(task));
        }
    }
}