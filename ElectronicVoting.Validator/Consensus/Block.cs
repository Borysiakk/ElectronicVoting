using System.Collections.Generic;

namespace ElectronicVoting.Validator.Consensus
{
    public class Block
    {
        public int Id { get; set; }
        public int Result { get; set; }
        public string Hash { get; set; }
        public string PreviousHash { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}