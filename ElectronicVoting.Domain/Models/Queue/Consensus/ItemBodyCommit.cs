using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicVoting.Domain.Models.Queue.Consensus
{
    public class ItemBodyCommit : ItemBody
    {
        public byte[] Hash { get; set; }
    }
}
