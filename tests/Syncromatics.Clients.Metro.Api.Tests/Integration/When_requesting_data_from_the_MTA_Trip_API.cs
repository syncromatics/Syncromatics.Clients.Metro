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
                ServerRootUrl = "http://not-actual-dev.metro.net/",
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
    }
}
