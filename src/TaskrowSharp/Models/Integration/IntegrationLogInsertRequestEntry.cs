using System;
using System.Text.Json.Serialization;

namespace TaskrowSharp.Models.Integration;

public class IntegrationLogInsertRequestEntry
{
    [JsonPropertyName("userMessage")]
    public string UserMessage { get; set; }

    [JsonPropertyName("userDetail")]
    public string UserDetail { get; set; }

    [JsonPropertyName("techDetail")]
    public string TechDetail { get; set; }

    [JsonPropertyName("eventDate")]
    public DateTimeOffset EventDate { get; set; }

    [JsonPropertyName("logLevel")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public IntegrationLogEntryLevels LogLevel { get; set; }

    public IntegrationLogInsertRequestEntry()
    {

    }

    public IntegrationLogInsertRequestEntry(string userMessage, string userDetail, string techDetail, DateTimeOffset eventDate, IntegrationLogEntryLevels logLevel)
    {
        UserMessage = userMessage;
        UserDetail = userDetail;
        TechDetail = techDetail;
        EventDate = eventDate;
        LogLevel = logLevel;
    }
}
