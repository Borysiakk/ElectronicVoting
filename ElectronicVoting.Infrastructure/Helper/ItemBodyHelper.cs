using ElectronicVoting.Domain.Models.Queue.Consensus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicVoting.Infrastructure.Helper
{
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
}
