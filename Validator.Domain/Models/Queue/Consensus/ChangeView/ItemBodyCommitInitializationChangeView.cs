using Validator.Domain.Queue.Consensus;
namespace Validator.Domain.Models.Queue.Consensus.ChangeView;

public class ItemBodyCommitInitializationChangeView :ItemBody
{

    public int Round { get; set; }
    public int ApproverId { get; set; }
    public bool Decision { get; set; }
}
