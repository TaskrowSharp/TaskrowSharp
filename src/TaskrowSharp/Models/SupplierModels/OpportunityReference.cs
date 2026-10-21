using System.Text.Json.Serialization;

namespace TaskrowSharp.Models.SupplierModels;

public class OpportunityReference
{
    [JsonPropertyName("opportunityID")]
    public int OpportunityID { get; set; }

    [JsonPropertyName("opportunityNumber")]
    public int OpportunityNumber { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}
