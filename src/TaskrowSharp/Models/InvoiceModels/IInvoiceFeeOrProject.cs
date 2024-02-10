using System.Text.Json.Serialization;
using System;
using TaskrowSharp.JsonConverters;

namespace TaskrowSharp.Models.InvoiceModels;

public interface IInvoiceFeeOrProject
{
    [JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
    public DateTime? IADueDate { get; set; }

    [JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
    public DateTime? InvoiceIssueDate { get; set; }

    public decimal IAValue { get; set; }

    public int InvoiceID { get; set; }

    public string InvoiceNumber { get; set; }

    public int IAStatusID { get; set; }

    public string IAMemo { get; set; }

    public string IAAdmMemo { get; set; }
}
