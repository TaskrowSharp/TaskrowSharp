using System;

namespace TaskrowSharp.Models
{
    public class SaveInvoiceRequest
    {
        public int InvoiceID { get; private set; }
        public int InvoiceTypeID { get; private set; }
        public string GuidModification { get; private set; }
        public int ClientAddressID { get; private set; }
        public int FromClientAddressID { get; private set; }
        
        public string InvoiceMemo { get; set; }

        public string? InvoiceNumber { get; set; }

        public DateTime? InvoiceIssueDate { get; set; }

        public int? InvoiceServiceCodeID { get; set; }
        public decimal? InvoiceGrossValue { get; set; }

        public decimal? InvoiceNetValue { get; set; }

        public decimal? InvoiceDeductionValue { get; set; }

        public decimal? InvoiceTaxValue { get; set; }
        public int? InvoiceTaxValueID { get; set; }

        public decimal? DirectTax1AliquotValue { get; set; }

        public decimal? DirectTax1Value { get; set; }

        public decimal? InvoiceTax1Value { get; set; }
        public int? InvoiceTax1ValueID { get; set; }

        public decimal? InvoiceTax2Value { get; set; }
        public int? InvoiceTax2ValueID { get; set; }

        public SaveInvoiceRequest(int invoiceID, int invoiceTypeID, string guidModification, int clientAddressID, int fromClientAddressID)
        {
            this.InvoiceID = invoiceID;
            this.InvoiceTypeID = invoiceTypeID;
            this.GuidModification = guidModification;
            this.ClientAddressID = clientAddressID;
            this.FromClientAddressID = fromClientAddressID;
        }
    }
}
