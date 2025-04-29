using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TaskrowSharp.Models.Integration;

public class IntegrationLogListResponse
{
    [JsonPropertyName("logEntries")]
    public List<IntegrationLogEntry> LogEntries { get; set; } = [];
}
