using RestSharp;

namespace Syncromatics.Clients.Metro.Api
{
    public class MetroJsonRequest : RestRequest
    {
        public MetroJsonRequest(string resource, Method method)
            : base(resource, method)
        {
            JsonSerializer = new JsonNetSerializer();
            RequestFormat = DataFormat.Json;
        }
    }
}