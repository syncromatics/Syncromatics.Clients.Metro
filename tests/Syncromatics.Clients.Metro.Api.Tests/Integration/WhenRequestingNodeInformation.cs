using System.Collections.Generic;
using System.Linq;
using ExpressMapper.Extensions;
using FluentAssertions;
using Syncromatics.Clients.Metro.Api.Models;
using Xunit;

namespace Syncromatics.Clients.Metro.Api.Tests.Integration
{
    public class WhenRequestingNodeInformation : WithMetroApiClient
    {
        [Theory]
        [InlineData("11130-MB")]
        [InlineData("11130")]
        [InlineData("11131")]
        public async void ItShouldGetInformationForAKnownNodeId(string nodeId)
        {
            var nodes = await Subject.GetNodeInformationByNodeId(nodeId);

            nodes.Should().NotBeNull();
            nodes.Should().NotBeEmpty();
            nodes.Select(nt => nt.CarrierName.ToLower()).Should().IntersectWith(new[] { "metro" });
        }

        [Theory]
        [InlineData("30002")]
        [InlineData("12530")]
        [InlineData("9333")]
        [InlineData("15718")]
        [InlineData("1646")]
        public async void ItShouldGetInformationForAKnownStopId(string stopId)
        {
            var nodes = await Subject.GetNodeInformationByStopId(stopId);

            nodes.Should().NotBeNull();
            nodes.Should().NotBeEmpty();
            nodes.Select(st => st.CarrierName.ToLower()).Should().IntersectWith(new[] { "metro" });
        }

        [Theory]
        [InlineData("11129")]
        [InlineData("INVALID_NODE_ID")]
        [InlineData("0")]
        public async void ItShouldNotGetInformationForAnUnknownNode(string nodeId)
        {
            var nodes = await Subject.GetNodeInformationByNodeId(nodeId);

            nodes.Should().NotBeNull();
            nodes.Should().BeEmpty();
        }

        [Theory]
        [InlineData("15718")]
        [InlineData("1646")]
        public async void ItShouldGetMoreInformationThanArrivalTimes(string stopId)
        {
            var nodes = await Subject.GetNodeInformationByStopId(stopId);
            var times = await Subject.GetStopTimes(stopId);

            nodes.Should().NotBeNullOrEmpty();
            times.Should().NotBeNullOrEmpty();

            nodes.Count.Should().BeGreaterThan(times.Count);

            var nodesForTimes = nodes.Join(
                times,
                n => $"{n.RouteId}:{n.Sign}",
                t => $"{t.RouteId}:{t.Sign}",
                (n, t) => n);

            nodesForTimes.ShouldAllBeEquivalentTo(times.Map(new List<Node>()), config => config.Excluding(x => x.Title));
        }
    }
}
