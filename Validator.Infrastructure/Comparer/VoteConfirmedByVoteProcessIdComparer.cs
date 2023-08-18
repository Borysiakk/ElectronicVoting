using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validator.Domain.Table.Election;

namespace Validator.Infrastructure.Comparer
{
    public class VoteConfirmedByVoteIdComparer : IEqualityComparer<VoteConfirmed>
    {
        public bool Equals(VoteConfirmed? x, VoteConfirmed? y)
        {
            return x.VoteProcessId == y.VoteProcessId;
        }

        public int GetHashCode([DisallowNull] VoteConfirmed obj)
        {
            return obj.VoteProcessId.GetHashCode();
        }
    }
}
