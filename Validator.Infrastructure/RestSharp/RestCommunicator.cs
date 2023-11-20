using RestSharp;

namespace Validator.Infrastructure.RestSharp;

public interface IRestCommunicator
{
    Task<RestResponse> GetAsync(string serverUrl, string relativeUrl, CancellationToken cancellationToken);
    Task<RestResponse> GetAsync(string serverUrl, string relativeUrl, Dictionary<string, string> parameters, CancellationToken cancellationToken);
    Task<RestResponse> PostAsync<T>(string serverUrl, string relativeUrl, T model, CancellationToken cancellationToken) where T : class;
}

public class RestCommunicator : IRestCommunicator
{
    public async Task<RestResponse> GetAsync(string serverUrl, string relativeUrl, CancellationToken cancellationToken)
    {
        var client = new RestClient(serverUrl);
        var request = new RestRequest(relativeUrl, Method.Get);

        return await client.GetAsync(request, cancellationToken);
    }

    public async Task<RestResponse> GetAsync(string serverUrl, string relativeUrl, Dictionary<string, string> parameters, CancellationToken cancellationToken)
    {
        var client = new RestClient(serverUrl);
        var request = new RestRequest(relativeUrl, Method.Get);

        foreach (var parameter in parameters)
            request.AddParameter(parameter.Key, parameter.Value);

        return await client.GetAsync(request, cancellationToken);
    }

    public async Task<RestResponse> PostAsync<T>(string serverUrl, string relativeUrl, T model, CancellationToken cancellationToken) where T : class
    {
        var client = new RestClient(serverUrl);
        var request = new RestRequest(relativeUrl, Method.Post);

        request.AddJsonBody(model);
        return await client.ExecutePostAsync(request, cancellationToken);
    }
}
