using System.Text.Json.Serialization;

namespace TaskrowSharp.Models.Integration;

public class IntegrationLogInsertRequest
{
    [JsonPropertyName("entityType")]
    public string EntityType { get; set; }

    [JsonPropertyName("entityID")]
    public string EntityID { get; set; }

    [JsonPropertyName("entry")]
    public IntegrationLogInsertRequestEntry Entry { get; set; }

    public IntegrationLogInsertRequest()
    {

    }

    public IntegrationLogInsertRequest(string entityType, string entityID, IntegrationLogInsertRequestEntry entry)
    {
        EntityType = entityType;
        EntityID = entityID;
        Entry = entry;
    }
}
