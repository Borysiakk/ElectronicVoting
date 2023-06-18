using Validator.Domain.Queue.Consensus;
namespace Validator.Domain.Models.Queue.Consensus.ChangeView;

public class ItemBodyCommitInitializationChangeView :ItemBody
{
    public bool Decision { get; set; }
    public int Round { get; set; }
    public string UserName { get; set; }
}
