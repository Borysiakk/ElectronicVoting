using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicVoting.Domain.Table
{
    public class TransactionRegister : BaseEntity
    {
        public long Index { get; set; }
        public bool IsInserted { get; set; }
        public string TransactionId { get; set; }
    }
}
