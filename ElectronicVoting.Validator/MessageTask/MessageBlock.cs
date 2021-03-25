using System.Collections.Generic;
using ElectronicVoting.Validator.Consensus;
using ElectronicVoting.Validator.PriorityQueue;

namespace ElectronicVoting.Validator.MessageTask
{
    public class MessageBlock
    {
        public MessageBlock()
        {
            Validators = new List<string>();
        }
        
        public string To { get; set; }
        public string From { get; set; }
        public Stage Stage { get; set; }
        public Block Block { get; set; }
        public List<string> Validators { get; set; }
    }
}