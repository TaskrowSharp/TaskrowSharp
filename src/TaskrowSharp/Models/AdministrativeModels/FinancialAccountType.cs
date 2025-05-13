using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TaskrowSharp.Models.AdministrativeModels;

public class FinancialAccountType
{
    [JsonPropertyName("Name")]
    public string Name { get; set; }

    [JsonPropertyName("Value")]
    public int Value { get; set; }

    [JsonPropertyName("Description")]
    public string Description { get; set; }

    [JsonPropertyName("EnumValue")]
    public int EnumValue { get; set; }

    [JsonPropertyName("Attributes")]
    public List<FinancialAccountTypeAttribute>? Attributes { get; set; }
}