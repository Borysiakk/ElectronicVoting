using ElectronicVoting.Common.Domain;

namespace Validator.Domain.Table.ChangeView;

public class Round : BaseEntity
{
    public int Index { get; set; }
    public int ApproverId { get; set; }
    public virtual Approver Approver { get; set; }
}
