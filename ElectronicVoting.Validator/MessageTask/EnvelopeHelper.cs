using System.Text;
using ElectronicVoting.Validator.PriorityQueue;
using Newtonsoft.Json;

namespace ElectronicVoting.Validator.MessageTask
{
    public class EnvelopeHelper
    {
        public static byte[] CreateMessage(MessageBlock message, PriorityMessage priority = PriorityMessage.Normal)
        {
            Envelope envelope = new Envelope()
            {
                Priority = priority,
                Task = JsonConvert.SerializeObject(message),
            };
            
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(envelope));
        }
    }
}