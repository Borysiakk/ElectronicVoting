using ElectronicVoting.Common.Domain;

namespace Validator.Domain.Table.ChangeView;

public class PreElectionVote :BaseEntity
{
    public int Round { get; set; }
    public bool Decision { get; set; }
    public string TransactionId { get; set; }
    public Int64 ApproverId { get; set; }
    public virtual Approver Approver { get; set; }
}
