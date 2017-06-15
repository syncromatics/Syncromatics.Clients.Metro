using Newtonsoft.Json;

namespace Syncromatics.Clients.Metro.Api.Models
{
    public class NodeTime
    {
        public string Route { get; set; }
        public string Title { get; set; }
        public string Sign { get; set; }
        [JsonProperty("abbrev_rte")]
        public string RouteAbbreviation { get; set; }
        [JsonProperty("carrier_id")]
        public string CarrierId { get; set; }
        [JsonProperty("carrier_name")]
        public string CarrierName { get; set; }
        public string Direction { get; set; }
        public string Location { get; set; }
        public string Bay { get; set; }
        [JsonProperty("stop_id")]
        public string StopId { get; set; }
        public string Times { get; set; }
        [JsonProperty("icon")]
        public string IconUrl { get; set; }
    }
}