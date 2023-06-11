
using ElectronicVoting.Common.Domain;

namespace ElectronicVoting.Validator.Domain.Table;

public class TransactionPending : BaseEntity
{
    public byte[] Hash { get; set; }
    public string TransactionId { get; set; }
}
