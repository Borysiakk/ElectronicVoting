using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicVoting.Domain.Models.Queue.Consensus
{
    public class ItemBodyPrePrepare :ItemBody
    {
        public long Voice { get; set; }
    }
}
