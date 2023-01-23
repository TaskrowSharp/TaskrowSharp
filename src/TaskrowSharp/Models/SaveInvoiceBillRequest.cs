using System;
using System.Text.Json.Serialization;
using TaskrowSharp.JsonConverters;

namespace TaskrowSharp.Models
{
    public class SaveInvoiceBillRequest
    {
        public int InvoiceID { get; set; }
        
        public int InvoiceBillID { get; set; }
        
        public string? GuidModification { get; set; }
        
        public int BillTypeID { get; set; }
        
        public decimal BillValue { get; set; }
        
        public int FinancialAccountID { get; set; }

        public int ClientAddressID { get; set; }
        
        [JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
        public DateTime? DueDate { get; set; }

        public string? Memo { get; set; }

        [JsonConverter(typeof(DateTimeNullableTaskrowFormatJsonConverter))]
        public DateTime? MemoDueDate { get; set; }

        public SaveInvoiceBillRequest(int invoiceID, int billTypeID, decimal billValue, int financialAccountID,
            int clientAddressID, DateTime? dueDate)
        {
            this.InvoiceID = invoiceID;
            this.BillTypeID = billTypeID;
            this.BillValue = billValue;
            this.FinancialAccountID = financialAccountID;
            this.ClientAddressID = clientAddressID;
            this.DueDate = dueDate;
        }
    }
}
