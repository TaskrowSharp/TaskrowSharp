using System;
using System.Text.Json.Serialization;

namespace TaskrowSharp.Models.Integration;

public class IntegrationLogInsertRequestEntry
{
    [JsonPropertyName("logLevel")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public IntegrationLogEntryLevels LogLevel { get; set; }

    [JsonPropertyName("eventDate")]
    public DateTimeOffset EventDate { get; set; }

    [JsonPropertyName("userMessage")]
    public string UserMessage { get; set; }

    [JsonPropertyName("userDetail")]
    public string? UserDetail { get; set; }

    [JsonPropertyName("techDetail")]
    public string? TechDetail { get; set; }

    public IntegrationLogInsertRequestEntry()
    {

    }

    public IntegrationLogInsertRequestEntry(IntegrationLogEntryLevels logLevel, DateTimeOffset eventDate, string userMessage, string? userDetail, string? techDetail)
    {
        LogLevel = logLevel;
        EventDate = eventDate;
        UserMessage = userMessage;
        UserDetail = userDetail;
        TechDetail = techDetail;
    }
}
