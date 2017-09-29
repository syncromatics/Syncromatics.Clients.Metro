using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestEase;
using Syncromatics.Clients.Metro.Api.Models;

namespace Syncromatics.Clients.Metro.Api
{
    [Header("User-Agent", "Syncromatics.Clients.Metro")]
    public interface IMetroApi
    {
        [Get("/api/node_time.php?format=json")]
        Task<NodeTimesResponse> GetNodeTimes(string node_id, string getonoff = "on");

        [Get("/api/node_time.php?format=json")]
        Task<NodeTimesResponse> GetStopTimes(string stop_id, string getonoff = "on");

        [Get("/api/node_time.php?format=json&notime=true")]
        Task<NodesResponse> GetNodes(string node_id, string getonoff = "on");

        [Get("/api/node_time.php?format=json&notime=true")]
        Task<NodesResponse> GetStops(string stop_id, string getonoff = "on");

        [Get("/API/=stops_SYNC/Stops_N_Node.php?output_format=json&minifyresult=true")]
        Task<List<Stop>> GetStops(string query_type, string node_IDWC = null, string node = null, string stop_id = null, string carrier_code = null);
        
        [Get("/API/=stops_SYNC/Stops_SYNC.php?query_type=routebased&output_format=json&minifyresult=true")]
        Task<List<Stop>> GetStopsByRoute(string RTE);
    }
}