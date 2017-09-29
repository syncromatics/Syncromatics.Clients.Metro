using System.Collections.Generic;
using Newtonsoft.Json;

namespace Syncromatics.Clients.Metro.Api.Models
{
    public class NodesResponse
    {
        [JsonProperty("node_time")]
        public List<Node> Nodes { get; set; } = new List<Node>();
    }
}