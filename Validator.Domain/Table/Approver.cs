using ElectronicVoting.Common.Domain;
using Validator.Domain.Table.ChangeView;

namespace Validator.Domain.Table;

public class Approver :BaseEntity
{
    public String Name { get; set; }
    public String Address { get; set; }

    public virtual ICollection<Round> Rounds { get; set; }
    public virtual ICollection<PreElectionVote> PreElectionVote { get; set; }
    public virtual ICollection<ElectionVote> ElectionVotes { get; set; }
    public virtual ICollection<ElectionVote> SelectedElectionVotes { get; set; }
}
