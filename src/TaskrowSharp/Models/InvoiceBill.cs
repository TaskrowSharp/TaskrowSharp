using System;
using System.Text.Json.Serialization;
using TaskrowSharp.JsonConverters;

namespace TaskrowSharp.Models
{
    public class InvoiceBill
    {
        public int InvoiceBillID { get; set; }
        
        public int InvoiceID { get; set; }

        [JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
        public DateTime? PaymentDate { get; set; }
        
        public decimal? BillValue { get; set; }

        public string BillNumber { get; set; }
        
        public bool IsCancelled { get; set; }
        
        public bool IsDeleted { get; set; }

        [JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
        public DateTime? BillDueDate { get; set; }

        public InvoiceBillDueDate InvoiceBillDueDate { get; set; }
    }
}
