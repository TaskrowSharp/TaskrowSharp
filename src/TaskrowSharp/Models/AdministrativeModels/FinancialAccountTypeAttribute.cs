using System.Text.Json.Serialization;

namespace TaskrowSharp.Models.AdministrativeModels;

public class FinancialAccountTypeAttribute
{
    [JsonPropertyName("Description")]
    public string Description { get; set; }

    [JsonPropertyName("TypeId")]
    public string TypeId { get; set; }
}