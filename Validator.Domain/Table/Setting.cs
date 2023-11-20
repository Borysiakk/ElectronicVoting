namespace Validator.Domain.Table;

public class Setting
{
    public long SettingId { get; set; }
    public string Category { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
    public string ValueType { get; set; }
}
