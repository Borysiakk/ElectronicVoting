using System.Diagnostics.CodeAnalysis;
using Validator.Domain.Table.Electronic;

namespace Validator.Domain.Comparer;

public class VoteConfirmedBySessionElectionIdComparer : IEqualityComparer<VoteConfirmed>
{
    public bool Equals(VoteConfirmed? x, VoteConfirmed? y)
    {
        return x.SessionElectionId == y.SessionElectionId;
    }

    public int GetHashCode([DisallowNull] VoteConfirmed obj)
    {
        return obj.SessionElectionId.GetHashCode();
    }
}
