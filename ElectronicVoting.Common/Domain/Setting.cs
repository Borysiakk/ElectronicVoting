namespace ElectronicVoting.Common.Domain.Table;

public class Setting :BaseEntity
{
    public string Name { get; set; }
    public string SubName { get; set; }
    public string ? Value { get; set; }
}
