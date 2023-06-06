using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicVoting.Domain.Table.Main
{
    public class Validator :BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string ConnectionString { get; set; }
        public string ConnectionStringToBuild { get; set; }
    }
}
