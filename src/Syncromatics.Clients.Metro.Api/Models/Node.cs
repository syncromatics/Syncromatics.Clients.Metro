using Newtonsoft.Json;

namespace Syncromatics.Clients.Metro.Api.Models
{
    public class Node
    {
        public string Route { get; set; }
        public string Title { get; set; }
        public string Sign { get; set; }

        [JsonProperty("abbrev_rte")]
        public string RouteAbbreviation { get; set; }

        public string CarrierId { get; set; }

        public string CarrierName { get; set; }

        public string Direction { get; set; }
        public string Location { get; set; }
        public string Bay { get; set; }
        public string Display { get; set; }

        [JsonProperty("route_identifier")]
        public string RouteId { get; set; }

        public string StopId { get; set; }

        [JsonProperty("icon")]
        public string IconUrl { get; set; }

        [JsonProperty("audio")]
        public string AudioUrl { get; set; }

        public string Version { get; set; }

        [JsonProperty("node")]
        public string NodeId { get; set; }
        public string Corner { get; set; }
    }
}