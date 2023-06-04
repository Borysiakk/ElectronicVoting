using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicVoting.Domain.Handler.Command.Consensu
{
    public class PrePrepare : IRequest
    {
        public long Voice { get; set; }
    }
}
