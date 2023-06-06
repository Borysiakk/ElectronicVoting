using Microsoft.EntityFrameworkCore;

using Index = Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ElectronicVoting.Domain.Table.Main
{
    public class Setting :BaseEntity
    {
        public string Name { get; set; }
        public string SubName { get; set; }
        public string ? Value { get; set; }
    }
}
