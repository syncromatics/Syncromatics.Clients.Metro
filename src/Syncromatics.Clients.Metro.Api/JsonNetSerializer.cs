using Newtonsoft.Json;
using RestSharp.Serializers;

namespace Syncromatics.Clients.Metro.Api
{
    internal class JsonNetSerializer : ISerializer
    {
        public JsonNetSerializer()
        {
            ContentType = "application/json";
        }

        public string ContentType { get; set; }

        public string DateFormat { get; set; }

        public string Namespace { get; set; }

        public string RootElement { get; set; }

        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
