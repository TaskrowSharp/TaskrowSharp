using System;
using System.Text.Json.Serialization;

namespace TaskrowSharp.Models
{
    public class InsertInvoiceRequest
    {
        public int InvoiceFeeID { get; set; }
        public int FeeID { get; set; }
        public int GuidModification { get; set; }

        [JsonPropertyName("jobNumber")]
        public int JobNumber { get; private set; }
        
        public string FeePeriod { get; private set; }

        public string InvoiceForecast { get; private set; }

        [JsonPropertyName("FromClientAddressID")]
        public int FromClientAddressID { get; private set; }

        [JsonPropertyName("ClientAddressID")]
        public int ToClientAddressID { get; set; }

        public decimal MonthRevenueValue { get; set; }

        public decimal MonthExpectedValue { get; set; }
        
        public decimal InvoiceIAValue { get; set; }
        
        public decimal IAValue { get; set; }

        public string IADueDate { get; set; }

        public int IAStatusID { get; set; }

        public int UserSigningDocument { get; set; }

        public string IAMemo { get; set; }

        public string? IAAdmMemo { get; set; }

        public InsertInvoiceRequest(int jobNumber, int feeMonth, int feeYear, int invoiceForecastMonth, int invoiceForecastYear, 
            int fromClientAddressID, int toClientAddressID,
            decimal monthRevenueValue, decimal monthExpectedValue, decimal invoiceValue,
            DateTime dueDate, 
            int invoiceStatusID, int userSigningDocument,
            string description,
            string? administrativeNotes)
        {
            JobNumber = jobNumber;
            FeePeriod = new DateTime(feeYear, feeMonth, 1).ToString("MM/yyyy");
            InvoiceForecast = new DateTime(invoiceForecastYear, invoiceForecastMonth, 1).ToString("MM/yyyy");
            FromClientAddressID = fromClientAddressID;
            ToClientAddressID = toClientAddressID;
            MonthRevenueValue = monthRevenueValue;
            MonthExpectedValue = monthExpectedValue;
            InvoiceIAValue = invoiceValue;
            IAValue = invoiceValue;
            IADueDate = dueDate.ToString("dd/MM/yyyy");
            IAStatusID = invoiceStatusID;
            UserSigningDocument = userSigningDocument;
            IAMemo = description;
            IAAdmMemo = administrativeNotes;
        }
    }
}
