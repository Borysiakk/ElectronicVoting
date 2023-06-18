using ElectronicVoting.Common.Domain;

namespace Validator.Domain.Table;
public class TransactionRegister : BaseEntity
{
    public long Index { get; set; }
    public bool IsInserted { get; set; }
    public string TransactionId { get; set; }
}
