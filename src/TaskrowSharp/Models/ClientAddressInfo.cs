using System.Text.Json.Serialization;

namespace TaskrowSharp.Models;

public class ClientAddressInfo
{
    [JsonPropertyName("ClientAddressID")]
    public int ClientAddressID { get; set; }

    [JsonPropertyName("ClientID")]
    public int ClientID { get; set; }

    [JsonPropertyName("SocialContractName")]
    public string SocialContractName { get; set; }

    [JsonPropertyName("CNPJ")]
    public string CNPJ { get; set; }

    [JsonPropertyName("CPF")]
    public object CPF { get; set; }

    [JsonPropertyName("FlagMain")]
    public bool FlagMain { get; set; }

    [JsonPropertyName("Inactive")]
    public bool Inactive { get; set; }

    [JsonPropertyName("FormattedSocialName")]
    public string FormattedSocialName { get; set; }
}
