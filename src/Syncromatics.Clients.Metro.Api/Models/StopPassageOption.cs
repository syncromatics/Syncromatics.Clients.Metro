using System.Runtime.Serialization;

namespace Syncromatics.Clients.Metro.Api.Models
{
    public enum StopPassageOption
    {
        [EnumMember(Value = "yy")]
        BoardingAndDischarge,
        [EnumMember(Value = "yn")]
        BoardingOnly,
        [EnumMember(Value = "ny")]
        DischargeOnly,
        [EnumMember(Value = "nn")]
        Layover,
        [EnumMember(Value = "on")]
        AnyBoarding,
        [EnumMember(Value = "off")]
        AnyDischarge,
    }
}