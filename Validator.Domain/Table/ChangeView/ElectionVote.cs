using ElectronicVoting.Common.Domain;

namespace Validator.Domain.Table.ChangeView;

public class ElectionVote :BaseEntity
{
    public string TransactionId { get; set; }
    public int Round { get; set; }
    public Int64 ApproverId { get; set; }
    public Int64 SelectedApproverId { get; set; }

    public virtual Approver Approver { get; set; }
    public virtual Approver SelectedApprover { get; set; }
}
