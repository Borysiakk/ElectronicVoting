using MediatR;

namespace Validator.Application.Handler.Command.Election;

public class NotifyLeaderVoteVerificationCompleted :IRequest
{
    public byte[] Hash { get; set; }
    public bool ResultVerifyVote { get; set; }
    public string SessionElectionId { get; set; }

    public NotifyLeaderVoteVerificationCompleted(byte[] hash, bool resultVerifyVote, string sessionElectionId)
    {
        Hash = hash;
        ResultVerifyVote = resultVerifyVote;
        SessionElectionId = sessionElectionId;
    }
}

public class NotifyLeaderVoteVerificationCompletedHandler
{

}
