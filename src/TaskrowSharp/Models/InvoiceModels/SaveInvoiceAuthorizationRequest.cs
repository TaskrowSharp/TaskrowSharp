using System.Collections.Generic;

namespace TaskrowSharp.Models.InvoiceModels;

public class SaveInvoiceAuthorizationRequest
{
    public int JobNumber { get; set; }
    public int InvoiceID { get; set; }
    public string GuidModification { get; set; }
    public List<int> InvoiceFeeIDs { get; set; }

    public SaveInvoiceAuthorizationRequest(int jobNumber, int invoiceID, string guidModification, List<int> invoiceFeeIDs)
    {
        JobNumber = jobNumber;
        InvoiceID = invoiceID;
        GuidModification = guidModification;
        InvoiceFeeIDs = invoiceFeeIDs;
    }
}
