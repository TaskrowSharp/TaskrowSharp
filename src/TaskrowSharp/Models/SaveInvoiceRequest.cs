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

        public SaveInvoiceRequest(InvoiceDetail invoiceDetail)
        {
            this.InvoiceID = invoiceDetail.InvoiceID;
            this.InvoiceTypeID = invoiceDetail.InvoiceTypeID;
            this.GuidModification = invoiceDetail.GuidModification;
            this.ClientAddressID = invoiceDetail.ClientAddressID;
            this.FromClientAddressID = invoiceDetail.FromClientAddressID;
            this.InvoiceMemo = invoiceDetail.InvoiceMemo;

            this.InvoiceNumber = invoiceDetail.InvoiceNumber;

            this.InvoiceIssueDate = invoiceDetail.InvoiceIssueDate;

            this.InvoiceServiceCodeID = invoiceDetail.InvoiceServiceCodeID;
            this.InvoiceGrossValue = invoiceDetail.InvoiceGrossValue;

            this.InvoiceNetValue = invoiceDetail.InvoiceNetValue;

            this.InvoiceDeductionValue = invoiceDetail.InvoiceDeductionValue;

            this.InvoiceTaxValue = invoiceDetail.InvoiceTaxValue;
            //this.InvoiceTaxValueID = invoiceDetail.InvoiceTaxValueID;

            this.DirectTax1AliquotValue = invoiceDetail.DirectTax1AliquotValue;

            this.DirectTax1Value = invoiceDetail.DirectTax1Value;

            this.InvoiceTax1Value = invoiceDetail.InvoiceTax1Value;
            //this.InvoiceTax1ValueID = invoiceDetail.InvoiceTax1ValueID;

            this.InvoiceTax2Value = invoiceDetail.InvoiceTax2Value;
            //this.InvoiceTax2ValueID = invoiceDetail.InvoiceTax2ValueID;
        }
    }
}
