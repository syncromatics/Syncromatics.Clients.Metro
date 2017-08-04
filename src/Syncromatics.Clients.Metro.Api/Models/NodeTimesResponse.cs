using System.Collections.Generic;
using Newtonsoft.Json;

namespace Syncromatics.Clients.Metro.Api.Models
{
    /// <summary>
    /// Represents the collection of <see cref="NodeTime" /> for a given node
    /// </summary>
    public class NodeTimesResponse : BaseResponse
    {
        /// <summary>
        /// List of <see cref="NodeTime" /> for a given node
        /// </summary>
        [JsonProperty("node_time")]
        public List<NodeTime> NodeTimes { get; set; } = new List<NodeTime>();
    }
}