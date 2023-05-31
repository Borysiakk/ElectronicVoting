

namespace ElectronicVoting.Domain.Table
{
    public class TransactionPending : BaseEntity
    {
        public byte[] Hash { get; set; }
        public string TransactionId { get; set; }
    }
}
