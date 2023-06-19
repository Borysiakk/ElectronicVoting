using ElectronicVoting.Common.Domain;
namespace Validator.Domain.Table.ChangeView;

public class InitializationChangeViewTransaction :BaseEntity
{
    public int Round { get; set; }
    public bool Decision { get; set; }
    public string UserName { get; set; }
    public string TransactionId { get; set; }
    public int ApproverId { get; set; }

    public virtual Approver Approver { get; set; }
}
