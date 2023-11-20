using System.Numerics;

namespace Validator.Domain.Table.Electronic
{
    public class RegisteredVote
    {
        public long RegisteredVoteId { get; set; }
        public string Vote { get; set; }
        public string SessionElectionId { get; set; }

        public RegisteredVote()
        {
            Vote = string.Empty;
            SessionElectionId = string.Empty;
        }

        public RegisteredVote(string vote, string sessionElectionId)
        {
            Vote = vote;
            SessionElectionId = sessionElectionId;
        }
    }
}
