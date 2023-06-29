

using Validator.Domain.Queue.Consensus;

namespace Validator.Domain.Models.Queue.Consensus.ChangeView;

public class ItemBodyPreElectionPreparation : ItemBody
{
    public int Round { get; set; }
}
