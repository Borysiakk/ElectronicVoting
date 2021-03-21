using System.Collections.Generic;

namespace ElectronicVoting.Validator.Consensus
{
    public class Transaction
    {
        public string Id { get; set; }
        public int Voice { get; set; }
        public int ConnectionCount { get; set; }
        public List<string> Validations { get; set; }
        public Dictionary<string,bool> Confirmation { get; set; } 
    }
}