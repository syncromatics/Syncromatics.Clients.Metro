using System.Linq;
using FluentAssertions;
using Xunit;

namespace Syncromatics.Clients.Metro.Api.Tests.Integration
{
    public class WhenRequestingArrivalTimes : WithMetroApiClient
    {
        [Theory]
        [InlineData("11130-MB")]
        [InlineData("11130")]
        [InlineData("11131")]
        public async void ItShouldGetTimesForAKnownNodeId(string nodeId)
        {
            var times = await Subject.GetNodeTimes(nodeId);

            times.Should().NotBeNull();
            times.Should().NotBeEmpty();
            times.Select(nt => nt.CarrierName.ToLower()).Should().IntersectWith(new[] { "metro" });
        }

        [Theory]
        [InlineData("30002")]
        [InlineData("12530")]
        [InlineData("9333")]
        [InlineData("15718")]
        [InlineData("1646")]
        public async void ItShouldGetTimesForAKnownStopId(string stopId)
        {
            var times = await Subject.GetStopTimes(stopId);

            times.Should().NotBeNull();
            times.Should().NotBeEmpty();
            times.Select(st => st.CarrierName.ToLower()).Should().IntersectWith(new[] {"metro"});
        }

        [Theory]
        [InlineData("11129")]
        [InlineData("INVALID_NODE_ID")]
        [InlineData("0")]
        public async void ItShouldNotGetTimesForAnUnknownNode(string nodeId)
        {
            var times = await Subject.GetNodeTimes(nodeId);

            times.Should().NotBeNull();
            times.Should().BeEmpty();
        }
    }
}