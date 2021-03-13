using System.Threading.Tasks;

namespace ElectronicVoting.SignalR.Interface
{
    public interface IConnectionVotingClient
    {
        public Task ReceiveToSuperValidatorVoting(string text);
    }
}