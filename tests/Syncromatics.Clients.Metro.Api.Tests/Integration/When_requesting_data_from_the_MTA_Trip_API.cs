using System;
using System.Linq;
using Xunit;
using FluentAssertions;

namespace Syncromatics.Clients.Metro.Api.Tests.Integration
{
    public class When_requesting_data_from_the_MTA_Trip_API
    {
        private readonly MetroApiClient _subject;

        public When_requesting_data_from_the_MTA_Trip_API()
        {
            var clientSettings = new ClientSettings
            {
                ServerRootUrl = Environment.GetEnvironmentVariable("TEST_URL") ?? "http://not-actual-dev.metro.net/",
            };
            _subject = new MetroApiClient(clientSettings);
        }

        [Theory]
        [InlineData("11130-MB")]
        [InlineData("11130")]
        [InlineData("11131")]
        public async void It_should_get_times_for_a_known_node(string nodeId)
        {
            var times = await _subject.GetNodeTimes(nodeId);

            times.Should().NotBeNull();
            times.Should().NotBeEmpty();
            times.Select(nt => nt.CarrierName).Should().IntersectWith(new[] { "Metro" });
        }

        [Theory]
        [InlineData("11129")]
        [InlineData("INVALID_NODE_ID")]
        [InlineData("0")]
        public async void It_should_not_get_times_for_an_unknown_node(string nodeId)
        {
            var times = await _subject.GetNodeTimes(nodeId);

            times.Should().NotBeNull();
            times.Should().BeEmpty();
        }

        [Theory]
        [InlineData("11130-MB", null)]
        [InlineData("11130", "MB")]
        [InlineData("11130", null)]
        [InlineData("11131", null)]
        public async void It_should_get_stops_for_known_node_with_corner(string nodeId, string corner)
        {
            var stops = await _subject.GetStopsByNodeId(nodeId, corner);

            stops.Should().NotBeNull();
            stops.Should().NotBeEmpty();
            stops.Select(x => x.NodeIdWithCorner).All(x => x.StartsWith(nodeId)).Should().BeTrue();
        }
    }
}
