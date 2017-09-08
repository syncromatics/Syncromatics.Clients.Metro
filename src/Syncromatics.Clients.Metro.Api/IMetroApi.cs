using System.Collections.Generic;
using System.Threading.Tasks;
using RestEase;
using Syncromatics.Clients.Metro.Api.Models;

namespace Syncromatics.Clients.Metro.Api
{
    public interface IMetroApi
    {
        [Get("/api/node_time.php")]
        Task<NodeTimesResponse> GetNodeTimes(string node_id, string format = "json");
        
        [Get("/api/node_time.php")]
        Task<NodeTimesResponse> GetStopTimes(string stop_id, string format = "json");
        
        [Get("/API/=stops_SYNC/Stops_N_Node.php")]
        Task<List<Stop>> GetStops(string query_type, string node_IDWC = null, string node = null, string stop_id = null, string carrier_code = null, string minifyresult = "true", string output_format = "json");
        
        [Get("/API/=stops_SYNC/Stops_SYNC.php")]
        Task<List<Stop>> GetStopsByRoute(string RTE, string query_type = "routebased", string minifyresult = "true", string output_format = "json");
    }
}