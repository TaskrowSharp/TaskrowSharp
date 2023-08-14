using System;
using System.Text.Json.Serialization;
using TaskrowSharp.JsonConverters;

namespace TaskrowSharp.Models
{
    public class SaveInvoiceBillPaymentRequest
    {
        public int InvoiceBillID { get; set; }

        public string GuidModification { get; set; }

        [JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
        public DateTime? PaymentDate { get; set; }

        public decimal PaymentValueRef { get; set; }

        public decimal PaymentDiscountValue { get; set; }

        public decimal PaymentValueDisplay { get; set; }

        public decimal PaymentValue { get; set; }

        public string? PaymentMemo { get; set; }

        public SaveInvoiceBillPaymentRequest(int invoiceBillID, string? guidModification,
            DateTime? paymentDate, decimal paymentValueRef, decimal paymentDiscountValue, decimal paymentValueDisplay,
            decimal paymentValue, string? paymentMemo) { 
            this.InvoiceBillID = invoiceBillID;
            this.GuidModification = guidModification;
            this.PaymentDate = paymentDate;
            this.PaymentValueRef = paymentValueRef;
            this.PaymentDiscountValue = paymentDiscountValue;
            this.PaymentValueDisplay = paymentValueDisplay;
            this.PaymentValue = paymentValue;
            this.PaymentMemo = paymentMemo;
        }
    }
}
