using System.Text.Json.Serialization;

namespace TaskrowSharp.Models.ClientModels;

public class ClientItemSearch
{
    [JsonPropertyName("value")]
    public string Value { get; set; }

    [JsonPropertyName("ClientName")]
    public string ClientName { get; set; }

    [JsonPropertyName("ClientID")]
    public int ClientID { get; set; }

    [JsonPropertyName("ClientNickName")]
    public string ClientNickName { get; set; }

    [JsonPropertyName("Inactive")]
    public bool Inactive { get; set; }
}
