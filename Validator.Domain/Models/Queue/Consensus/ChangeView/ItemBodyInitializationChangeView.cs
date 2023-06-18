using ElectronicVoting.Validator.Domain.Queue.Consensus;
namespace Validator.Domain.Models.Queue.Consensus;
public class ItemBodyInitializationChangeView : ItemBody
{
    public int Round { get; set; }
    public bool Decision { get; set; }
}
