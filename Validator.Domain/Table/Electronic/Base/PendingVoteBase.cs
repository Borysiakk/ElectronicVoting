namespace Validator.Domain.Table.Electronic.Base
{
    public abstract class PendingVoteBase
    {
        public byte[] Hash { get; set; }
        public bool ResultVerifyVote { get; set; }
        public string SessionElectionId { get; set; }
    }
}
