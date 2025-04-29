using System;
using System.Text.Json.Serialization;

namespace TaskrowSharp.Models.Integration;

public class IntegrationLogEntry
{
    [JsonPropertyName("logID")]
    public string LogID { get; set; }

    [JsonPropertyName("userMessage")]
    public string UserMessage { get; set; }

    [JsonPropertyName("userDetail")]
    public string UserDetail { get; set; }

    [JsonPropertyName("techDetail")]
    public string TechDetail { get; set; }

    [JsonPropertyName("eventDate")]
    public DateTimeOffset EventDate { get; set; }

    [JsonPropertyName("logLevel")]
    public string LogLevel { get; set; }

    public IntegrationLogEntry()
    {

    }

    public IntegrationLogEntry(string userMessage, string userDetail, string techDetail, DateTimeOffset eventDate, string logLevel)
    {
        UserMessage = userMessage;
        UserDetail = userDetail;
        TechDetail = techDetail;
        EventDate = eventDate;
        LogLevel = logLevel;
    }
}
