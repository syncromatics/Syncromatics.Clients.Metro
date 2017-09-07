using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using Syncromatics.Clients.Metro.Api.Models;

namespace Syncromatics.Clients.Metro.Api
{
    public class MetroApiClient : BaseClient, IMetroApiClient
    {
        public MetroApiClient(ClientSettings clientSettings)
            : base(clientSettings)
        { }

        public async Task<List<NodeTime>> GetNodeTimes(string nodeId)
        {
            var response = await ExecuteAsync<NodeTimesResponse>(new MetroJsonRequest("/api/node_time.php", Method.GET)
                .AddQueryParameter("node_id", nodeId)
                .AddQueryParameter("format", "json"));

            return response.NodeTimes;
        }

        public async Task<List<Stop>> GetStopsByNodeId(string nodeId, string corner = null)
        {
            var request = new MetroJsonRequest("/API/=stops_SYNC/Stops_N_Node.php", Method.GET)
                .AddQueryParameter("query_type", "nodebased")
                .AddQueryParameter("minifyresult", "false")
                .AddQueryParameter("output_format", "json");

            if (nodeId.Contains("-"))
            {
                request.AddQueryParameter("node_IDWC", nodeId);
            }
            else if (!string.IsNullOrWhiteSpace(corner))
            {
                request.AddQueryParameter("node_IDWC", $"{nodeId}-{corner}");
            }
            else
            {
                request.AddQueryParameter("node", nodeId);
            }

            var response = await ExecuteAsync<List<Stop>>(request);
            return response;
        }
        
        public async Task<List<Stop>> GetStopsByStopId(string stopId, string carrier = null)
        {
            var request = new MetroJsonRequest("/API/=stops_SYNC/Stops_N_Node.php", Method.GET)
                .AddQueryParameter("stop_id", stopId)
                .AddQueryParameter("query_type", "stopbased")
                .AddQueryParameter("minifyresult", "false")
                .AddQueryParameter("output_format", "json");

            if (!string.IsNullOrWhiteSpace(carrier))
            {
                request.AddQueryParameter("carrier_code", carrier);
            }

            var response = await ExecuteAsync<List<Stop>>(request);
            return response;
        }

        public async Task<List<Stop>> GetStopsByRouteId(string routeId)
        {
            var request = new MetroJsonRequest("/API/=stops_SYNC/Stops_SYNC.php", Method.GET)
                .AddQueryParameter("RTE", routeId)
                .AddQueryParameter("query_type", "routebased")
                .AddQueryParameter("minifyresult", "false")
                .AddQueryParameter("output_format", "json");

            var response = await ExecuteAsync<List<Stop>>(request);
            return response;
        }
    }
}