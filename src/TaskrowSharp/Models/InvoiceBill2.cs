using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using TaskrowSharp.JsonConverters;

namespace TaskrowSharp.Models
{
    public class InvoiceBill2
    {
        public Invoice Invoice { get; set; }
        public int InvoiceBillID { get; set; }
        public int InvoiceID { get; set; }
        public string BillNumber { get; set; }
        //public object BillValue { get; set; }

        [JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
        public DateTime? DateCreation { get; set; }

        [JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
        public DateTime? DateModification { get; set; }

        [JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
        public DateTime? DateCancel { get; set; }

        [JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
        public DateTime? PaymentDate { get; set; }

        public decimal? PaymentValue { get; set; }
        public string? PaymentMemo { get; set; }
        public string? Memo { get; set; }
        public int? BillTypeID { get; set; }
        public string BillType { get; set; }
        public bool IsCancelled { get; set; }
        public bool IsDeleted { get; set; }
        public int? FinancialAccountID { get; set; }
        //public object FinancialAccount { get; set; }
        public int ClientAddressID { get; set; }
        public ClientAddress ClientAddress { get; set; }
        public string GuidModification { get; set; }
        public string? CancellationMemo { get; set; }

        [JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
        public DateTime? BillDueDate { get; set; }

        //NOTE: This is the difference to InvoiceBill.cs, here is an array!!!
        public List<InvoiceBillDueDate> InvoiceBillDueDate { get; set; }

        public decimal? PaymentDiscountValue { get; set; }
        public bool CanBuildBillet { get; set; }
    }
}
