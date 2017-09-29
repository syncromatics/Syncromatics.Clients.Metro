using System;

namespace Syncromatics.Clients.Metro.Api.Tests.Integration
{
    public class WithMetroApiClient
    {
        protected MetroApiClient Subject;

        public WithMetroApiClient()
        {
            var clientSettings = new ClientSettings
            {
                ServerRootUrl = Environment.GetEnvironmentVariable("TEST_URL") ?? "http://lacmta-api.metrocloudalliance.com/",
            };
            Subject = new MetroApiClient(clientSettings);
        }
    }
}