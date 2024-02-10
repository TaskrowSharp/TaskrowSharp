using System.Text.Json.Serialization;

namespace TaskrowSharp.Models.ClientModels;

public class ListClientItem
{
    [JsonPropertyName("clientID")]
    public int ClientID { get; set; }

    [JsonPropertyName("clientNickname")]
    public string ClientNickname { get; set; }

    [JsonPropertyName("displayName")]
    public string DisplayName { get; set; }

    [JsonPropertyName("inactive")]
    public bool Inactive { get; set; }
}
