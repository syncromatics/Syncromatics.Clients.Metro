using System.Collections.Generic;
using Newtonsoft.Json;

namespace Syncromatics.Clients.Metro.Api.Models
{
    public class NodeTimesResponse
    {
        [JsonProperty("node_time")]
        public List<NodeTime> NodeTimes { get; set; } = new List<NodeTime>();
    }
}