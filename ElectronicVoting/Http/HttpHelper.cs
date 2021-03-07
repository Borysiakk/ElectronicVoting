using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace ElectronicVoting.Http
{
    public static class HttpHelper
    {
        public static async Task<T> Post<T,G>(string url,G model)
        {
            RestClient restClient = new RestClient(Routes.Root);
            
            var request = new RestRequest(url, Method.POST);
            request.AddHeader("Content-type", "application/json");
            request.AddJsonBody(model);
            
            IRestResponse restResponse = await restClient.ExecuteAsync<T>(request);
            return JsonConvert.DeserializeObject<T>(restResponse.Content);
        }
    }
}