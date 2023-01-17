using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using TaskrowSharp.JsonConverters;

namespace TaskrowSharp.Models
{
    public class InvoiceFee : IInvoiceFeeOrProject
    {
        public int InvoiceFeeID { get; set; }

        public int FeeID { get; set; }
        
        public int FeeMonth { get; set; }

        public int FeeYear { get; set; }

        public int FeeMonthReference { get; set; }

        public string? FeeMonthLabel { get; set; }

        public int ClientAddressID { get; set; }

        [JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
        public DateTime? IADueDate { get; set; }

        public decimal IAValue { get; set; }

        public string? IAMemo { get; set; }

        [JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
        public DateTime? DateCreation { get; set; }

        public string? IAAdmMemo { get; set; }
        
        public int IAStatusID { get; set; }

        public string? IAStatus { get; set; }

        public UserReference UserModification { get; set; }
        
        public bool EditAllowed { get; set; }
        
        public JobReference? Job { get; set; }

        public int UserSigningDocument { get; set; }

        public int AFNumber { get; set; }

        public string InvoiceFeeNumber { get; set; }

        public int InvoiceID { get; set; }
        
        public string? InvoiceNumber { get; set; }

        [JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
        public DateTime? InvoiceIssueDate { get; set; }

        public bool InvoiceIsCancelled { get; set; }

        public bool InvoiceIsDeleted { get; set; }
        
        public List<InvoiceBill> InvoiceBill { get; set; }

        public string InvoiceForecast { get; set; }
    }
}
