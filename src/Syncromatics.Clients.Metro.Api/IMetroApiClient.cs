using System.Collections.Generic;
using System.Threading.Tasks;
using Syncromatics.Clients.Metro.Api.Models;

namespace Syncromatics.Clients.Metro.Api
{
    /// <summary>
    /// Client to interact with the Metro API
    /// </summary>
    public interface IMetroApiClient
    {
        /// <summary>
        /// Get arrival times for a Metro node
        /// </summary>
        /// <param name="nodeId">ID of the Metro node</param>
        /// <returns>List of arrival times for the given <paramref name="nodeId"/></returns>
        Task<List<NodeTime>> GetNodeTimes(string nodeId);
    }
}