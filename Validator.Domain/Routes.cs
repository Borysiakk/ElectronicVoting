﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validator.Domain
{
    public static class Routes
    {
        public const string Reply = "api/PbftConsensus/Reply";
        public const string Commit = "api/PbftConsensus/Commit";
        public const string Prepare = "api/PbftConsensus/Prepare";
        public const string PrePrepare = "api/PbftConsensus/Pre-Prepare";

        public const string RegisterVote = "/api/Election/RegisterVote";

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
