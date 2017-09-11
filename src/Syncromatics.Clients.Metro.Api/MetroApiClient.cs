using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestEase;
using Syncromatics.Clients.Metro.Api.Models;

namespace Syncromatics.Clients.Metro.Api
{
    public class MetroApiClient : IMetroApiClient
    {
        private readonly IMetroApi _client;

        public MetroApiClient(ClientSettings clientSettings)
        {
            _client = new RestClient(clientSettings.ServerRootUrl)
            {
            }.For<IMetroApi>();
        }

        public async Task<List<NodeTime>> GetNodeTimes(string nodeId)
        {
            var result = await _client.GetNodeTimes(nodeId);

            return result.NodeTimes;
        }

        public async Task<List<NodeTime>> GetStopTimes(string stopId)
        {
            var result = await _client.GetStopTimes(stopId);

            return result.NodeTimes;
        }

        public Task<List<Stop>> GetStopsByNodeId(string nodeId, string corner = null)
        {
            string node = null;
            string nodeWithCorner = null;
            if (nodeId.Contains("-"))
            {
                nodeWithCorner = nodeId;
            }
            else if (!string.IsNullOrWhiteSpace(corner))
            {
                nodeWithCorner = $"{nodeId}-{corner}";
            }
            else
            {
                node = nodeId;
            }

            return _client.GetStops("nodebased", node_IDWC: nodeWithCorner, node: node);
        }

        public Task<List<Stop>> GetStopsByStopId(string stopId, string carrier = null)
        {
            return _client.GetStops("stopbased", stop_id: stopId, carrier_code: carrier);
        }

        public Task<List<Stop>> GetStopsByRouteId(string routeId)
        {
            return _client.GetStopsByRoute(routeId);
        }
    }
}