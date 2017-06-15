using System;
using System.Net;
using System.Threading.Tasks;
using RestSharp;

namespace Syncromatics.Clients.Metro.Api
{
    public class BaseClient
    {
        protected readonly ClientSettings ClientSettings;

        public BaseClient(ClientSettings clientSettings)
        {
            ClientSettings = clientSettings;
        }

        protected async Task<T> ExecuteAsync<T>(IRestRequest request)
            where T : new()
        {
            IRestClient client = new RestClient()
            {
                BaseUrl = new Uri(ClientSettings.ServerRootUrl),
                Timeout = (int)TimeSpan.FromMinutes(5).TotalMilliseconds,
            };
            client.AddHandler("application/json", new JsonNetDeserializer());

            var tcs = new TaskCompletionSource<T>();
            client.ExecuteAsync(request, (IRestResponse<T> response) =>
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        tcs.SetResult(response.Data);
                        break;
                    default:
                        tcs.SetException(new Exception(response.Content));
                        break;
                }
            });

            return await tcs.Task;
        }
    }
}