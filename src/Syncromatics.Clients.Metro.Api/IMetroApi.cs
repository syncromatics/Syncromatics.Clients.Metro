using System.Collections.Generic;
using System.Threading.Tasks;
using Syncromatics.Clients.Metro.Api.Models;
using RestEase;

namespace Syncromatics.Clients.Metro.Api
{
    /// <summary>
    /// Client to interact with the Metro API
    /// </summary>
    public interface IMetroApi
    {
        /// <summary>
        /// Get arrival times for a Metro node
        /// </summary>
        /// <param name="nodeId">ID of the Metro node</param>
        /// <returns>List of node times for the given <paramref name="nodeId"/></returns>
        [Get("/api/node_time.php?format=json")]
        Task<NodeTimesResponse> GetNodeTimes([Query("node_id")] string nodeId);
    }
}