using System.ComponentModel;
using System.Text.Json.Serialization;
using TaskrowSharp.JsonConverters;

namespace TaskrowSharp.Models.Integration;

[JsonConverter(typeof(EnumTextJsonConverter<IntegrationLogEntryLevels>))]
public enum IntegrationLogEntryLevels
{
    [Description("info")]
    Info = 1,

    [Description("warning")]
    Warning = 2,

    [Description("error")]
    Error = 3
}
