using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicVoting.Domain.Handler.Command.Consensu
{
    public class Commit : IRequest
    {
        public byte[] Hash { get; set; }
        public string TransactionId { get; set; }
    }
}
