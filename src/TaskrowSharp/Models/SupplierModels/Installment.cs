using System;
using System.Text.Json.Serialization;

namespace TaskrowSharp.Models.SupplierModels;

public class Installment
{
    [JsonPropertyName("expenseEntryID")]
    public int ExpenseEntryID { get; set; }

    [JsonPropertyName("supplierOrderID")]
    public int SupplierOrderID { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("value")]
    public double Value { get; set; }

    [JsonPropertyName("dueDate")]
    public DateTime DueDate { get; set; }

    [JsonPropertyName("invoiceToClientAddressID")]
    public int InvoiceToClientAddressID { get; set; }

    [JsonPropertyName("clientID")]
    public int ClientID { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("supplierInvoiceID")]
    public object SupplierInvoiceID { get; set; }

    [JsonPropertyName("supplierInvoice")]
    public object SupplierInvoice { get; set; }

    [JsonPropertyName("hasInvoiceProject")]
    public bool HasInvoiceProject { get; set; }

    [JsonPropertyName("hasPayment")]
    public bool HasPayment { get; set; }

    [JsonPropertyName("hasInvoiceFeeSupplierIncentives")]
    public bool HasInvoiceFeeSupplierIncentives { get; set; }

    [JsonPropertyName("integrationStatusID")]
    public object IntegrationStatusID { get; set; }
}
