using System;

namespace TaskrowSharp.Models.InvoiceModels;

public class InvoiceSaveRequest
{
    public int InvoiceID { get; private set; }
    public int InvoiceTypeID { get; private set; }
    public string GuidModification { get; private set; }
    public int ClientAddressID { get; private set; }
    public int FromClientAddressID { get; private set; }

    public string InvoiceMemo { get; set; }

    public string InvoiceNumber { get; set; }

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

    public InvoiceSaveRequest(int invoiceID, int invoiceTypeID, string guidModification, int clientAddressID, int fromClientAddressID)
    {
        InvoiceID = invoiceID;
        InvoiceTypeID = invoiceTypeID;
        GuidModification = guidModification;
        ClientAddressID = clientAddressID;
        FromClientAddressID = fromClientAddressID;
    }

    public InvoiceSaveRequest(Invoice invoiceDetail)
    {
        InvoiceID = invoiceDetail.InvoiceID;
        InvoiceTypeID = invoiceDetail.InvoiceTypeID;
        GuidModification = invoiceDetail.GuidModification;
        ClientAddressID = invoiceDetail.ClientAddressID;
        FromClientAddressID = invoiceDetail.FromClientAddressID;
        InvoiceMemo = invoiceDetail.InvoiceMemo;

        InvoiceNumber = invoiceDetail.InvoiceNumber;

        InvoiceIssueDate = invoiceDetail.InvoiceIssueDate;

        InvoiceServiceCodeID = invoiceDetail.InvoiceServiceCodeID;
        InvoiceGrossValue = invoiceDetail.InvoiceGrossValue;

        InvoiceNetValue = invoiceDetail.InvoiceNetValue;

        InvoiceDeductionValue = invoiceDetail.InvoiceDeductionValue;

        InvoiceTaxValue = invoiceDetail.InvoiceTaxValue;
        //this.InvoiceTaxValueID = invoiceDetail.InvoiceTaxValueID;

        DirectTax1AliquotValue = invoiceDetail.DirectTax1AliquotValue;

        DirectTax1Value = invoiceDetail.DirectTax1Value;

        InvoiceTax1Value = invoiceDetail.InvoiceTax1Value;
        //this.InvoiceTax1ValueID = invoiceDetail.InvoiceTax1ValueID;

        InvoiceTax2Value = invoiceDetail.InvoiceTax2Value;
        //this.InvoiceTax2ValueID = invoiceDetail.InvoiceTax2ValueID;
    }
}
