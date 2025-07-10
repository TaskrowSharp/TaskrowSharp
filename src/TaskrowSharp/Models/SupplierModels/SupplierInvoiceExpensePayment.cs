using System.Collections.Generic;
using System.Text.Json.Serialization;
using TaskrowSharp.Models.UserModels;

namespace TaskrowSharp.Models.SupplierModels;

public class SupplierInvoiceExpensePayment
{
    [JsonPropertyName("ExpensePaymentID")]
    public int? ExpensePaymentID { get; set; }

    [JsonPropertyName("ExpensePaymentTypeID")]
    public int? ExpensePaymentTypeID { get; set; }

    [JsonPropertyName("ExpensePaymentType")]
    public string ExpensePaymentType { get; set; }

    [JsonPropertyName("PaymentTitle")]
    public string PaymentTitle { get; set; }

    [JsonPropertyName("LastModificationDate")]
    public string LastModificationDate { get; set; }

    [JsonPropertyName("PaymentDate")]
    public string PaymentDate { get; set; }

    [JsonPropertyName("PaymentTo")]
    public object? PaymentTo { get; set; }

    [JsonPropertyName("PaymentValue")]
    public double? PaymentValue { get; set; }

    [JsonPropertyName("DiscountValue")]
    public double? DiscountValue { get; set; }

    [JsonPropertyName("Memo")]
    public string Memo { get; set; }

    [JsonPropertyName("ExpensesAmount")]
    public int? ExpensesAmount { get; set; }

    [JsonPropertyName("Expenses")]
    public List<SupplierExpense> Expenses { get; set; }

    [JsonPropertyName("EntriesListString")]
    public string EntriesListString { get; set; }

    [JsonPropertyName("User")]
    public User User { get; set; }

    [JsonPropertyName("FinancialAccountID")]
    public int? FinancialAccountID { get; set; }

    [JsonPropertyName("SupplierID")]
    public int? SupplierID { get; set; }

    [JsonPropertyName("Supplier")]
    public Supplier Supplier { get; set; }

    [JsonPropertyName("Attachments")]
    public List<object> Attachments { get; set; }
}
