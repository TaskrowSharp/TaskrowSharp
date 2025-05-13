using System.Text.Json.Serialization;

namespace TaskrowSharp.Models.BasicDataModels;

public class Bank
{
    [JsonPropertyName("BankID")]
    public int BankID { get; set; }

    [JsonPropertyName("BankNumber")]
    public int BankNumber { get; set; }

    [JsonPropertyName("Name")]
    public string Name { get; set; }

    [JsonPropertyName("PortfolioCode")]
    public string PortfolioCode { get; set; }

    [JsonPropertyName("CovenantCode")]
    public string CovenantCode { get; set; }

    [JsonPropertyName("FormattedName")]
    public string FormattedName { get; set; }
}
