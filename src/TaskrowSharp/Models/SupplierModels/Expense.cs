using System;
using System.Text.Json.Serialization;
using TaskrowSharp.JsonConverters;
using TaskrowSharp.Models.ClientModels;
using TaskrowSharp.Models.JobModels;

namespace TaskrowSharp.Models.SupplierModels;

public class Expense
{
    public int ExpenseEntryID { get; set; }
    public string ExpenseTitle { get; set; }
    public decimal ExpenseValue { get; set; }
    public string ExpenseDate { get; set; }
    public int SupplierOrderID { get; set; }
    public int? AdOrderID { get; set; }

    [JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
    public DateTime? ExpenseDueDate { get; set; }

    public int RefundTypeID { get; set; }
    public int InvoiceToClientAddressID { get; set; }
    public InvoiceToClientAddress InvoiceToClientAddress { get; set; }
    public Job Job { get; set; }
    public Client Client { get; set; }
    public int? InvoiceProjectID { get; set; }
    //public List<object> Attachments { get; set; }
}
