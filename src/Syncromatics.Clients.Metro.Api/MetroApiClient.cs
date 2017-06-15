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
    }
}

namespace Syncromatics.Clients.Metro.Api.Models
{
}
/*
 route	"METRO EXPRESS LINE 460"
title	"DOWNTOWN LA - 6TH-LOS ANGELES"
sign	"DOWNTOWN LA - 6TH-LOS ANGELES"
abbrev_rte	"460"
carrier_id	"34"
carrier_name	"Metro"
direction	"W"
location	"E-6TH ST/HOPE ST&GRAND AV"
bay	""
stop_id	"15718"
times	"1,8,32"
icon	"http://mtatripdev01.metro.net/tm/logos/Metro-128x96.png"
audio	""
sym	""
     
     */
