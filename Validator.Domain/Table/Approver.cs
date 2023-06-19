using ElectronicVoting.Common.Domain;
using Validator.Domain.Table.ChangeView;

namespace Validator.Domain.Table;

public class Approver :BaseEntity
{
    public String Name { get; set; }
    public String Address { get; set; }

    public virtual ChangeViewTransaction ChangeViewTransaction { get; set; }
    public virtual InitializationChangeViewTransaction InitializationChangeViewTransaction { get; set; }
}
