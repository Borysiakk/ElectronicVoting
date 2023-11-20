namespace Validator.Domain
{
    public static  class Routes
    {
        public const string RegisterVote = "/api/Election/register-vote";
        public const string VerifyLocalVote = "/api/Election/verify-local-vote";
        public const string FinalizeLocalVoting = "/api/Election/finalize-local-vote";
        public const string NotifyLocalVoteVerificationCompleted = "/api/Election/notify-local-vote-verification-completed";
        public const string RecordAcceptedVote = "/api/Election/record-accepted-vote";

        public const string PreElectionVoteRecord = "api/Election/ChangeLeader/pre-election-vote-record";
        public const string PreElectionPreparation = "api/Election/ChangeLeader/pre-election-preparation";
        public const string PreElectionNotifyLeaderCompleted = "api/Election/ChangeLeader/pre-election-notify-leader-completed";

        public const string ElectionVoteRecord = "api/Election/ChangeLeader/election-vote-record";
        public const string ElectionPreparation = "api/Election/ChangeLeader/election-preparation";
        public const string ElectionNotifyLeaderCompleted = "api/Election/ChangeLeader/election-notify-leader-completed";
        public const string ElectionPreparationInitialization = "api/Election/ChangeLeader/election-preparation-initialization";

        public const string ElectionSetNewLeader = "api/Election/ChangeLeader/set-new-leader";
    }
}
