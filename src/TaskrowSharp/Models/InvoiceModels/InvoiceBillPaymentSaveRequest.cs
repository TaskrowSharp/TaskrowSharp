using System;
using System.Text.Json.Serialization;
using TaskrowSharp.JsonConverters;

namespace TaskrowSharp.Models.InvoiceModels;

public class InvoiceBillPaymentSaveRequest
{
    public int InvoiceBillID { get; set; }
    public string GuidModification { get; set; }

    [JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
    public DateTime? PaymentDate { get; set; }

    public decimal PaymentValueRef { get; set; }
    public decimal PaymentDiscountValue { get; set; }
    public decimal PaymentValueDisplay { get; set; }
    public decimal PaymentValue { get; set; }
    public string PaymentMemo { get; set; }

    public InvoiceBillPaymentSaveRequest(int invoiceBillID, string guidModification,
        DateTime? paymentDate, decimal paymentValueRef, decimal paymentDiscountValue, decimal paymentValueDisplay,
        decimal paymentValue, string paymentMemo)
    {
        InvoiceBillID = invoiceBillID;
        GuidModification = guidModification;
        PaymentDate = paymentDate;
        PaymentValueRef = paymentValueRef;
        PaymentDiscountValue = paymentDiscountValue;
        PaymentValueDisplay = paymentValueDisplay;
        PaymentValue = paymentValue;
        PaymentMemo = paymentMemo;
    }
}
