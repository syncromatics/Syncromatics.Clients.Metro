using System.Linq;
using FluentAssertions;
using Xunit;

namespace Syncromatics.Clients.Metro.Api.Tests.Integration
{
    public class WhenRequestingStops : WithMetroApiClient
    {
        [Theory]
        [InlineData("11130-MB", null)]
        [InlineData("10925", "NE")]
        [InlineData("11130", null)]
        [InlineData("11131", null)]
        public async void ItShouldGetStopsForKnownNodeWithCorner(string nodeId, string corner)
        {
            var stops = await Subject.GetStopsByNodeId(nodeId, corner);

            stops.Should().NotBeNull();
            stops.Should().NotBeEmpty();
            stops.Select(x => x.NodeIdWithCorner).All(x => x.StartsWith(nodeId)).Should().BeTrue();
        }
    }
}