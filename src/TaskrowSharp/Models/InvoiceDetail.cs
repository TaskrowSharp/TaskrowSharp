using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using TaskrowSharp.JsonConverters;

namespace TaskrowSharp.Models
{
    public class InvoiceDetail
    {
        public int InvoiceID { get; set; }

        public string? InvoiceNumber { get; set; }

        public decimal InvoiceGrossValue { get; set; }

        public decimal InvoiceTaxValue { get; set; }

        public string InvoiceMemo { get; set; }

        [JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
        public DateTime? InvoiceDueDate { get; set; }

        [JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
        public DateTime? InvoiceIssueDate { get; set; }

        public decimal InvoiceTax1Value { get; set; }

        public decimal InvoiceTax2Value { get; set; }

        public bool IsCancelled { get; set; }

        public bool IsDeleted { get; set; }

        public bool Approved { get; set; }

        public int InvoiceTypeID { get; set; }

        public string InvoiceType { get; set; }

        [JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
        public DateTime? DateModification { get; set; }

        public decimal InvoiceNetValue { get; set; }

        public decimal DirectTax1Value { get; set; }

        public decimal InvoiceDeductionValue { get; set; }

        public decimal DirectTax1AliquotValue { get; set; }

        public string? DeductionMemo { get; set; }

        public string? CancellationMemo { get; set; }
        
        [JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
        public DateTime? DateCancel { get; set; }
        
        public string GuidModification { get; set; }

        public InvoiceDetailValues Values { get; set; }

        public UserReference UserModification { get; set; }

        //public object UserCancel { get; set; }

        public int ClientID { get; set; }

        public Client Client { get; set; }

        public int ClientAddressID { get; set; }

        public ClientAddress ClientAddress { get; set; }

        public int FromClientAddressID { get; set; }

        public FromClientAddress FromClientAddress { get; set; }

        public int InvoiceServiceCodeID { get; set; }

        public InvoiceServiceCode InvoiceServiceCode { get; set; }

        public List<InvoiceBill2> InvoiceBill { get; set; }

        public InvoiceAuthorization InvoiceAuthorization { get; set; }

        //public List<object> InvoiceTaxValueInvoice { get; set; }

        //public List<object> Attachments { get; set; }

        //public object InvoiceEmailHistory { get; set; }

        public int IntegrationStatusID { get; set; }

        public string IntegrationStatus { get; set; }
    }
}
