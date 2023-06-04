using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicVoting.Domain.Handler.Command.Consensu
{
    public class Prepare : IRequest
    {
        public long Voice { get; set; }
        public string TransactionId { get; set; }
    }

}
