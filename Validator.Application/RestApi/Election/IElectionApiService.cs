using Refit;
using Validator.Application.Handler.Command.Election;

namespace Validator.Application.RestApi.Election;

public interface IElectionApiService
{
    [Post("/api/Election/register-vote")]
    public Task RegisterVote([Body] RegisterVote data);

    [Post("/api/Election/verify-local-vote")]
    public Task VerifyLocalVote([Body] VerifyLocalVote data);

    [Post("/api/Election/finalize-local-vote")]
    public Task FinalizeLocalVote([Body] FinalizeLocalVote date);

    [Post("/api/Election/notify-local-vote-verification-completed")]
    public Task NotifyLocalVoteVerificationCompleted([Body] NotifyLocalVoteVerificationCompleted data);

    [Post("/api/Election/notify-leader-vote-verification-completed")]
    public Task NotifyLeaderVoteVerificationCompleted([Body] NotifyLeaderVoteVerificationCompleted data);

    [Post("/api/Election/record-accepted-vote")]
    public Task RecordAcceptedVote([Body] RecordAcceptedVote data);
}
