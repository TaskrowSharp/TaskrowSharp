using System.Text.Json.Serialization;
using TaskrowSharp.Models.BasicDataModels;

namespace TaskrowSharp.Models.ClientModels;

public class ClientAddress
{
    [JsonPropertyName("ClientAddressID")]
    public int ClientAddressID { get; set; }

    [JsonPropertyName("ClientID")]
    public int ClientID { get; set; }

    [JsonPropertyName("Client")]
    public Client Client { get; set; }

    [JsonPropertyName("NoCNPJ")]
    public bool NoCNPJ { get; set; }

    [JsonPropertyName("Country")]
    public Country? Country { get; set; }

    [JsonPropertyName("CNPJ")]
    public string CNPJ { get; set; }

    [JsonPropertyName("CPF")]
    public string CPF { get; set; }

    [JsonPropertyName("SocialContractName")]
    public string SocialContractName { get; set; }

    [JsonPropertyName("InscrEstad")]
    public string InscrEstad { get; set; }

    [JsonPropertyName("InscrMunic")]
    public string InscrMunic { get; set; }

    [JsonPropertyName("Street")]
    public string Street { get; set; }

    [JsonPropertyName("Number")]
    public string Number { get; set; }

    [JsonPropertyName("District")]
    public string District { get; set; }

    [JsonPropertyName("Complement")]
    public string Complement { get; set; }

    [JsonPropertyName("CityID")]
    public int CityID { get; set; }

    [JsonPropertyName("City")]
    public City? City { get; set; }

    [JsonPropertyName("CityName")]
    public string CityName { get; set; }

    [JsonPropertyName("StateName")]
    public string StateName { get; set; }

    [JsonPropertyName("ZipCode")]
    public string ZipCode { get; set; }

    [JsonPropertyName("CanDelete")]
    public bool CanDelete { get; set; }

    [JsonPropertyName("Location")]
    public string Location { get; set; }

    [JsonPropertyName("FlagMain")]
    public bool FlagMain { get; set; }

    [JsonPropertyName("Inactive")]
    public bool Inactive { get; set; }

    [JsonPropertyName("ExternalCode")]
    public string ExternalCode { get; set; }

    [JsonPropertyName("FormattedAddress")]
    public string FormattedAddress { get; set; }

    [JsonPropertyName("FormattedLocality")]
    public string FormattedLocality { get; set; }

    [JsonPropertyName("FormattedSocialName")]
    public string FormattedSocialName { get; set; }

    //[JsonPropertyName("Product")]
    //public List<object>? Product { get; set; }

    [JsonPropertyName("Complete")]
    public bool Complete { get; set; }

    [JsonPropertyName("ProductListString")]
    public string ProductListString { get; set; }

    [JsonPropertyName("ProductListNames")]
    public string ProductListNames { get; set; }

    //[JsonPropertyName("ClientAddressBankingInfo")]
    //public object ClientAddressBankingInfo { get; set; }
}
