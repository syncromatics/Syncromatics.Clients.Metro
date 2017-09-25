using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
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
                JsonSerializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy(),
                    },
                    Converters =
                    {
                        new StringEnumConverter(),
                    }
                },
            }.For<IMetroApi>();
        }

        public async Task<List<NodeTime>> GetNodeTimes(string nodeId, StopPassageOption stopPassageOption = StopPassageOption.AnyBoarding)
        {
            var result = await _client.GetNodeTimes(nodeId, ToEnumValue(stopPassageOption));

            return result.NodeTimes;
        }

        public async Task<List<NodeTime>> GetStopTimes(string stopId, StopPassageOption stopPassageOption = StopPassageOption.AnyBoarding)
        {
            var result = await _client.GetStopTimes(stopId, ToEnumValue(stopPassageOption));

            return result.NodeTimes;
        }

        public async Task<List<Node>> GetNodeInformationByNodeId(string nodeId, StopPassageOption stopPassageOption = StopPassageOption.AnyBoarding)
        {
            var result = await _client.GetNodes(nodeId, ToEnumValue(stopPassageOption));

            return result.Nodes;
        }

        public async Task<List<Node>> GetNodeInformationByStopId(string stopId, StopPassageOption stopPassageOption = StopPassageOption.AnyBoarding)
        {
            var result = await _client.GetStops(stopId, ToEnumValue(stopPassageOption));

            return result.Nodes;
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

        private static string ToEnumValue(Enum stopPassageOption)
        {
            var memberName = stopPassageOption.ToString();
            var declaredMembers = stopPassageOption.GetType()
                .GetTypeInfo()
                .DeclaredMembers;
            var customAttributes = declaredMembers
                .Where(m => m.Name == memberName)
                .SelectMany(m => m.CustomAttributes);
            var enumMemberAttributes = customAttributes
                .Where(a => a.AttributeType == typeof(EnumMemberAttribute))
                .SelectMany(a => a.NamedArguments)
                .Where(na => na.MemberName == "Value")
                .Select(na => na.TypedValue.Value as string);
            return enumMemberAttributes.SingleOrDefault() ?? memberName;
        }
    }
}