using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestEase;

namespace Syncromatics.Clients.Metro.Api
{
    /// <summary>
    /// Client to interact with the Metro API
    /// </summary>
    public static class MetroClient
    {
        /// <summary>
        /// JSON serialization settings
        /// </summary>
        public static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings()
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy(),
            },
        };

        /// <summary>
        /// Gets an instance of the <see cref="IMetroApi" />
        /// </summary>
        public static IMetroApi GetInstance(string url)
        {
            var api = new RestClient(url)
            {
                JsonSerializerSettings = JsonSerializerSettings
            }.For<IMetroApi>();

            return api;
        }
    }
}
