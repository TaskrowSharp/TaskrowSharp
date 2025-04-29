using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TaskrowSharp.Models.ClientModels;

public class ClientListItemResponse
{
    [JsonPropertyName("items")]
    public List<ClientListItem> Items { get; set; } = [];

    [JsonPropertyName("nextToken")]
    public string NextToken { get; set; }
}
