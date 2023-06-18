using ElectronicVoting.Common.Domain;

namespace Validator.Domain.Table;
public class TransactionConfirmed :BaseEntity
    {
        public long Voice { get; set; }
        public byte[] Hash { get; set; }
        public bool IsInserted { get; set; }
        public string TransactionId { get; set; }
    }