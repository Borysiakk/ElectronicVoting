using ElectronicVoting.Common.Domain;

namespace Validator.Domain.Table.ChangeView;

public class ChangeViewTransaction :BaseEntity
{
    public string TransactionId { get; set; }
    public int Round { get; set; }
    public int? ApproverId { get; set; }
    public int? SelectedApproverId { get; set; }

    public virtual Approver Approver { get; set; }
    public virtual Approver SelectedApprover { get; set; }
}
