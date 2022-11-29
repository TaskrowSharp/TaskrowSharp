using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TaskrowSharp.Models
{
    public class ListClientItemResponse
    {
        [JsonPropertyName("items")]
        public List<ListClientItem> Items { get; set; }

        [JsonPropertyName("nextToken")]
        public string? NextToken { get; set; }
    }
}
