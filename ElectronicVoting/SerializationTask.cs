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
        public static void SerializationPreparatory(byte[] bytes)
        {
            
        }

        public static byte[] SerializeTaskObjectToByte(TaskObject task)
        {
            string json = JsonConvert.SerializeObject(task);
            return Convert.FromBase64String(json);
        }
        
        public static KeyValuePair<Priority,NodePriorityQueue> DeserializePreparatory(byte[] bytes)
        {
            string json = System.Text.Encoding.UTF8.GetString(bytes);
            var task = JsonConvert.DeserializeObject<TaskJson>(json);

            return new KeyValuePair<Priority, NodePriorityQueue>
            (task.Priority,new NodePriorityQueue() {TaskJson = task.TaskPacked});
        }

        public async Task<TaskObject> DeserializeTask(string task)
        {
            return await Task.Run(() => JsonConvert.DeserializeObject<TaskObject>(task));
        }
    }
}