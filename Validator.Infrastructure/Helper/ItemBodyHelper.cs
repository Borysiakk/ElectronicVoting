using ElectronicVoting.Validator.Domain.Queue.Consensus;
using Newtonsoft.Json;


namespace ElectronicVoting.Validator.Infrastructure.Helper;

public static class ItemBodyHelper
{
    public static string SerializeJson(this ItemBody item)
    {
        return JsonConvert.SerializeObject(item, Formatting.Indented);
    }

    public static T? DeserializeObject<T>(string json) where T : ItemBody
    {
        return JsonConvert.DeserializeObject<T>(json);
    }
}
