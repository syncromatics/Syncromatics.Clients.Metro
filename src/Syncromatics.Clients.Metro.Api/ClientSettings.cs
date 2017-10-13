namespace Syncromatics.Clients.Metro.Api
{
    public class ClientSettings
    {
        public string ServerRootUrl { get; set; } = "http://lacmta-api.metrocloudalliance.com/";
        public int MaxConnections { get; set; } = 2;
    }
}