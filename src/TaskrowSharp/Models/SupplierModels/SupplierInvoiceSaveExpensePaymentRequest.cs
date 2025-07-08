using System;
using System.Text.Json.Serialization;

namespace TaskrowSharp.Models.SupplierModels;

public class SupplierInvoiceSaveExpensePaymentRequest
{
    [JsonPropertyName("ExpensePaymentID")]
    public int ExpensePaymentID { get; set; }

    [JsonPropertyName("EntriesListString")]
    public string EntriesListString { get; set; }

    [JsonPropertyName("SupplierRequired")]
    public bool SupplierRequired { get; set; }

    [JsonPropertyName("rbtExpenseFormat")]
    public string RbtExpenseFormat { get; set; }

    [JsonPropertyName("txtSearch")]
    public string TxtSearch { get; set; }

    [JsonPropertyName("PaymentTitle")]
    public string PaymentTitle { get; set; }

    [JsonPropertyName("ExpensePaymentTypeID")]
    public int ExpensePaymentTypeID { get; set; }

    [JsonPropertyName("PaymentDate")]
    public DateTime PaymentDate { get; set; }

    [JsonPropertyName("ClientID")]
    public int ClientID { get; set; }

    [JsonPropertyName("FinancialAccountID")]
    public int FinancialAccountID { get; set; }

    [JsonPropertyName("Memo")]
    public string Memo { get; set; }

    [JsonPropertyName("PaymentValueSource")]
    public decimal PaymentValueSource { get; set; }

    [JsonPropertyName("DiscountValueDisabled")]
    public decimal DiscountValueDisabled { get; set; }

    [JsonPropertyName("DiscountValue")]
    public decimal DiscountValue { get; set; }

    [JsonPropertyName("PaymentValue")]
    public decimal PaymentValue { get; set; }
}
