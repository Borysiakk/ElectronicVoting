using RestSharp;

namespace ElectronicVoting.Common.Helper;
public class HttpHelper
{
    public static async Task<RestResponse> PostAsync<T>(string serverUrl, string relativeUrl, T model, CancellationToken cancellationToken) where T : class
    {
        try
        {
            var client = new RestClient(serverUrl);
            var request = new RestRequest(relativeUrl, Method.Post);

            request.AddJsonBody(model);
            return await client.ExecuteAsync(request);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static async Task<RestResponse> GetAsync(string serverUrl, string relativeUrl, CancellationToken cancellationToken)
    {
        try
        {
            var client = new RestClient(serverUrl);
            var request = new RestRequest(relativeUrl, Method.Get);

            return await client.GetAsync(request);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static async Task<RestResponse> GetAsyncWithParameters(string serverUrl, string relativeUrl, Dictionary<string, string> parameters, CancellationToken cancellationToken)
    {
        try
        {
            var client = new RestClient(serverUrl);
            var request = new RestRequest(relativeUrl, Method.Get);

            foreach (var parameter in parameters)
            {
                request.AddParameter(parameter.Key, parameter.Value);
            }

            return await client.GetAsync(request);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}