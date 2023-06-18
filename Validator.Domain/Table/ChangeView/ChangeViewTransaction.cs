using ElectronicVoting.Common.Domain;

namespace Validator.Domain.Table.ChangeView;

public class ChangeViewTransaction :BaseEntity
{
    public string TransactionId { get; set; }
    public int Round { get; set; }
    public byte[] Hash { get; set; }
    public int ApproverId { get; set; }
    public int SelectedApproverId { get; set; }
}
