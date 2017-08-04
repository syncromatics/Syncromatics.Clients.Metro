using Newtonsoft.Json;

namespace Syncromatics.Clients.Metro.Api.Models
{
    /// <summary>
    /// Represents a predicted or scheduled time of arrival of a vehicle at a location
    /// </summary>
    public class NodeTime
    {
        /// <summary>
        /// Name of the route that the vehicle serves
        /// </summary>
        public string Route { get; set; }

        /// <summary>
        /// The abbreviated name of the <see cref="Route" />
        /// </summary>
        [JsonProperty("abbrev_rte")]
        public string RouteAbbreviation { get; set; }

        /// <summary>
        /// Description of the pattern for the <see cref="Route" />
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The text displayed on the vehicle headsign
        /// </summary>
        public string Sign { get; set; }

        /// <summary>
        /// Identity of the transit carrier
        /// </summary>
        public string CarrierId { get; set; }

        /// <summary>
        /// Name of the transit carrier
        /// </summary>
        public string CarrierName { get; set; }

        /// <summary>
        /// URL for the icon of the transit carrier
        /// </summary>
        [JsonProperty("icon")]
        public string IconUrl { get; set; }

        /// <summary>
        /// Transit carrier-specific stop identifier
        /// </summary>
        public string StopId { get; set; }

        /// <summary>
        /// Direction indicating which side of a street or intersection the node represents
        /// </summary>
        public string Direction { get; set; }

        /// <summary>
        /// Description of the street or intersection the node represents
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Optional indicator of which transit center bay this node represents
        /// </summary>
        public string Bay { get; set; }

        /// <summary>
        /// Comma-separated list of arrival times in minutes
        /// </summary>
        public string Times { get; set; }
    }
}