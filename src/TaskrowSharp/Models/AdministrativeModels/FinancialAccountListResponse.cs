using System.Collections.Generic;
using System.Text.Json.Serialization;
using TaskrowSharp.Models.BasicDataModels;
using TaskrowSharp.Models.ClientModels;

namespace TaskrowSharp.Models.AdministrativeModels;

public class FinancialAccountListResponse
{
    [JsonPropertyName("ClientAddressList")]
    public List<ClientAddress>? ClientAddressList { get; set; }

    [JsonPropertyName("FinancialAccountList")]
    public List<FinancialAccount>? FinancialAccountList { get; set; }

    [JsonPropertyName("FinancialAccountTypeList")]
    public List<FinancialAccountType>? FinancialAccountTypeList { get; set; }

    [JsonPropertyName("HaveMainAccount")]
    public bool HaveMainAccount { get; set; }

    [JsonPropertyName("BankList")]
    public List<Bank> BankList { get; set; }
}