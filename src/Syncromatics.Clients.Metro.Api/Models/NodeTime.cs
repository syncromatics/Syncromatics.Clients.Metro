using Newtonsoft.Json;

namespace Syncromatics.Clients.Metro.Api.Models
{
    public class NodeTime : Node
    {
        public string Times { get; set; }

        public bool UsedSchedule { get; set; }
        public string RealtimeSource { get; set; }
    }
}