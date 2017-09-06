using Newtonsoft.Json;

namespace Syncromatics.Clients.Metro.Api.Models
{
    public class Stop
    {
        [JsonProperty("Carrier_code")]
        public string CarrierCode { get; set; }

        [JsonProperty("Location")]
        public string Location { get; set; }

        [JsonProperty("Corner")]
        public string Corner { get; set; }

        [JsonProperty("Stop_id")]
        public long StopId { get; set; }

        [JsonProperty("Node_IDWC")]
        public string NodeIdWithCorner { get; set; }

        [JsonProperty("X")]
        public long X { get; set; }

        [JsonProperty("Y")]
        public long Y { get; set; }

        [JsonProperty("Lat")]
        public double Latitude { get; set; }

        [JsonProperty("Lng")]
        public double Longitude { get; set; }

        [JsonProperty("Route")]
        public string Route { get; set; }
    }
}
