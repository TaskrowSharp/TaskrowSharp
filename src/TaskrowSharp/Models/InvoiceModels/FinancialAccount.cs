using System;
using System.Text.Json.Serialization;
using TaskrowSharp.JsonConverters;
using TaskrowSharp.Models.BasicDataModels;
using TaskrowSharp.Models.ClientModels;

namespace TaskrowSharp.Models.InvoiceModels;

public class FinancialAccount
{
    public int FinancialAccountID { get; set; }

    public string Name { get; set; }

    public int AppMainCompanyID { get; set; }

    public int ClientAddressID { get; set; }

    public int FinancialAccountTypeID { get; set; }

    public string FinancialAccountType { get; set; }

    public decimal BeginningBalance { get; set; }

    [JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
    public DateTime? DateBeginningBalance { get; set; }

    public bool MainAccount { get; set; }

    public int? PaymentDueDateDay { get; set; }

    public bool Inactive { get; set; }

    public ClientAddress ClientAddres { get; set; }

    public int? BankID { get; set; }

    public Bank Bank { get; set; }

    public string AgencyNumber { get; set; }

    public string AgencyDigit { get; set; }

    public string AccountNumber { get; set; }

    public string AccountDigit { get; set; }

    public bool BilletEnabled { get; set; }

    //public List<object> Members { get; set; }
}
