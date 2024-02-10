using System;
using System.Text.Json.Serialization;
using TaskrowSharp.JsonConverters;

namespace TaskrowSharp.Models.InvoiceModels;

public class InvoiceBill
{
    public int InvoiceBillID { get; set; }

    public int InvoiceID { get; set; }

    public string BillNumber { get; set; }

    public decimal? BillValue { get; set; }

    [JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
    public DateTime? BillDueDate { get; set; }

    public InvoiceBillDueDate InvoiceBillDueDate { get; set; }

    [JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
    public DateTime? DateCreation { get; set; }

    [JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
    public DateTime? DateModification { get; set; }

    [JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
    public DateTime? DateCancel { get; set; }

    [JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
    public DateTime? PaymentDate { get; set; }

    public bool IsCancelled { get; set; }

    public bool IsDeleted { get; set; }
}
