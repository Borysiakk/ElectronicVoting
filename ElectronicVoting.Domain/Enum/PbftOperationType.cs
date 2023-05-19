using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicVoting.Domain.Enum
{
    public enum PbftOperationType
    {
        PrePrepare,
        Prepare,
        Commit,
        Reply
    }
}
