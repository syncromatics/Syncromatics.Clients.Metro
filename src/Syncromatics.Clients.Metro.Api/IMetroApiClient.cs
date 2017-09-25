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
        /// <param name="stopPassageOption">Optional filter for the type of passenger passage allowed at the stop</param>
        /// <returns>List of arrival times for the given <paramref name="nodeId"/></returns>
        Task<List<NodeTime>> GetNodeTimes(string nodeId, StopPassageOption stopPassageOption = StopPassageOption.AnyBoarding);

        /// <summary>
        /// Get arrival times for a Metro stop
        /// </summary>
        /// <param name="stopId">ID of the Metro stop</param>
        /// <param name="stopPassageOption">Optional filter for the type of passenger passage allowed at the stop</param>
        /// <returns>List of arrival times fo rthe given <param name="stopId"/></returns>
        Task<List<NodeTime>> GetStopTimes(string stopId, StopPassageOption stopPassageOption = StopPassageOption.AnyBoarding);

        /// <summary>
        /// Get node information for a Metro node
        /// </summary>
        /// <param name="nodeId">ID of the Metro node</param>
        /// <param name="stopPassageOption">Optional filter for the type of passenger passage allowed at the stop</param>
        /// <returns>List of nodes for the given <paramref name="nodeId"/></returns>
        Task<List<Node>> GetNodeInformationByNodeId(string nodeId, StopPassageOption stopPassageOption = StopPassageOption.AnyBoarding);

        /// <summary>
        /// Get node information for a Metro stop
        /// </summary>
        /// <param name="stopId">ID of the Metro stop</param>
        /// <param name="stopPassageOption">Optional filter for the type of passenger passage allowed at the stop</param>
        /// <returns>List of nodes fo rthe given <param name="stopId"/></returns>
        Task<List<Node>> GetNodeInformationByStopId(string stopId, StopPassageOption stopPassageOption = StopPassageOption.AnyBoarding);

        /// <summary>
        /// Gets a list of stops for a Metro node
        /// </summary>
        /// <param name="nodeId">ID of the Metro node.  May optionally include corner in the format NODEID-CORNER, e.g. 1234-NE.</param>
        /// <param name="corner">Optional corner, e.g. NE</param>
        /// <returns>List of stops for the given <paramref name="nodeId"/> and <paramref name="corner"/></returns>
        Task<List<Stop>> GetStopsByNodeId(string nodeId, string corner = null);

        /// <summary>
        /// Gets a list of all stops along the given Metro route
        /// </summary>
        /// <param name="routeId">ID of the Metro route</param>
        /// <returns>List of stops for the given <paramref name="routeId"/></returns>
        Task<List<Stop>> GetStopsByRouteId(string routeId);

        /// <summary>
        /// Get a list of stops for a Metro Stop ID.  A stop is returned once for each route that services it.
        /// </summary>
        /// <param name="stopId">The Metro Stop ID to search for</param>
        /// <param name="carrier">Optional Metro Carier Code used to restrict results</param>
        /// <returns>List of stops, one for each route that services the given <paramref name="stopId"/>.  Restricted to specified Carrier Code, if any value is supplied to <paramref name="carrier"/>.</returns>
        Task<List<Stop>> GetStopsByStopId(string stopId, string carrier = null);
    }
}