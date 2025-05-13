using System.Text.Json.Serialization;
using TaskrowSharp.Models.BasicDataModels;
using TaskrowSharp.Models.ClientModels;

namespace TaskrowSharp.Models.AdministrativeModels;

public class FinancialAccount
{
    [JsonPropertyName("FinancialAccountID")]
    public int FinancialAccountID { get; set; }

    [JsonPropertyName("Name")]
    public string Name { get; set; }

    [JsonPropertyName("AppMainCompanyID")]
    public int AppMainCompanyID { get; set; }

    [JsonPropertyName("ClientAddressID")]
    public int ClientAddressID { get; set; }

    [JsonPropertyName("FinancialAccountTypeID")]
    public int FinancialAccountTypeID { get; set; }

    [JsonPropertyName("FinancialAccountType")]
    public string FinancialAccountType { get; set; }

    [JsonPropertyName("BeginningBalance")]
    public decimal BeginningBalance { get; set; }

    [JsonPropertyName("DateBeginningBalance")]
    public string DateBeginningBalance { get; set; }

    [JsonPropertyName("MainAccount")]
    public bool MainAccount { get; set; }

    [JsonPropertyName("PaymentDueDateDay")]
    public object PaymentDueDateDay { get; set; }

    [JsonPropertyName("Inactive")]
    public bool Inactive { get; set; }

    [JsonPropertyName("ClientAddres")]
    public ClientAddress? ClientAddress { get; set; }

    [JsonPropertyName("BankID")]
    public int BankID { get; set; }

    [JsonPropertyName("Bank")]
    public Bank Bank { get; set; }

    [JsonPropertyName("AgencyNumber")]
    public string AgencyNumber { get; set; }

    [JsonPropertyName("AgencyDigit")]
    public string AgencyDigit { get; set; }

    [JsonPropertyName("AccountNumber")]
    public string AccountNumber { get; set; }

    [JsonPropertyName("AccountDigit")]
    public string AccountDigit { get; set; }

    [JsonPropertyName("BilletEnabled")]
    public bool BilletEnabled { get; set; }

    //[JsonPropertyName("Members")]
    //public List<object> Members { get; set; }
}
